using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace MvcSolution.Infrastructure.Utilities.NPOI
{
    public class ExcelReporter
    {
        private readonly Stream _stream;
        public Workbook Workbook { get; set; }
        public Sheet CurrentSheet { get; private set; }
        public int BodyLineHeight { get; set; }

        public ExcelReporter(Stream stream)
        {
            this.Workbook = new HSSFWorkbook();
            _stream = stream;
            this.BodyLineHeight = 300;
            this.Initialize();
        }


        public void Export<T>(string sheetName, IList<T> items, Action<ColumnFactory<T>> columns)
            where T : class
        {
            var sheet = String.IsNullOrEmpty(sheetName)
                            ? this.Workbook.CreateSheet()
                            : this.Workbook.CreateSheet(sheetName);
            var factory = new ColumnFactory<T>();
            columns(factory);
            this.CurrentSheet = sheet;
            this.GenerateHeader(factory, sheet);
            this.GenerateItems(items, factory, sheet);

            var columnIndex = 0;
            foreach (var column in factory.Columns)
            {
                sheet.AutoSizeColumn(columnIndex);
                var width = column.ColumnWidth;
                if (width <= 0)
                {
                    if (column.WidthFunction != null)
                    {
                        var currentWidth = sheet.GetColumnWidth(columnIndex);
                        const int rate = ColumnBuilder<T>.ExcelWidthRate;
                        width = column.WidthFunction(currentWidth/rate)*rate;
                    }
                }
                if (width > 0)
                {
                    sheet.SetColumnWidth(columnIndex, width);
                }
                else
                {
                    sheet.SetColumnWidth(columnIndex, sheet.GetColumnWidth(columnIndex) + 500);
                }
                columnIndex++;
            }
            this.Save();
        }

        #region private methods

        private void Initialize()
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();

            si.Author = "MvcSolution";
            si.CreateDateTime = DateTime.Now;
            si.LastSaveDateTime = DateTime.Now;
            si.ApplicationName = "MvcSolution";
            si.Keywords = "MvcSolution";
            dsi.Company = "MvcSolution";


            var hssfWorkbook = this.Workbook as HSSFWorkbook;
            if (hssfWorkbook != null)
            {
                hssfWorkbook.SummaryInformation = si;
                hssfWorkbook.DocumentSummaryInformation = dsi;
            }
        }

        private void GenerateHeader<T>(ColumnFactory<T> factory, Sheet sheet, int startRowIndex = 0)
            where T : class
        {
            var headRow = sheet.CreateRow(startRowIndex);
            var columnIndex = 0;
            var headerStyle = this.Workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = HSSFColor.LIGHT_CORNFLOWER_BLUE.index;
            headerStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            var font = this.Workbook.CreateFont();
            font.Boldweight = 2330;
            headerStyle.SetFont(font);
            headerStyle.VerticalAlignment = VerticalAlignment.CENTER;
            headRow.Height = (short) (this.BodyLineHeight + 150);
            foreach (var column in factory.Columns)
            {
                var columnCell = headRow.CreateCell(columnIndex);
                columnCell.SetCellValue(column.ColumnTitle);
                columnCell.CellStyle = headerStyle;
                columnIndex++;
            }
            headRow.CreateCell(columnIndex);
        }

        private void GenerateItems<T>(IList<T> dataSource, ColumnFactory<T> factory, Sheet sheet,
                                      Int32 startRowIndex = 1)
            where T : class
        {
            if (dataSource == null || !dataSource.Any())
            {
                return;
            }

            var bodyStyle = this.Workbook.CreateCellStyle();
            bodyStyle.WrapText = true;
            bodyStyle.VerticalAlignment = VerticalAlignment.TOP;

            foreach (var item in dataSource)
            {
                Row row = sheet.CreateRow(startRowIndex);

                Int32 columnIndex = 0;
                startRowIndex++;
                var maxLines = 1;
                foreach (var column in factory.Columns)
                {
                    Cell cell = row.CreateCell(columnIndex);
                    cell.CellStyle = bodyStyle;
                    var cellValue = string.Empty;
                    if (column.CellLines != null)
                    {
                        var lines = column.CellLines(item);
                        maxLines = Math.Max(lines.Length, maxLines);
                        cellValue = string.Join("\n", lines);
                    }
                    else if (column.Property != null)
                    {
                        var propertyValue = column.Property(item);
                        if (propertyValue != null)
                        {
                            cellValue = propertyValue.ToString();
                        }
                    }
                    cell.SetCellValue(cellValue);
                    columnIndex++;
                }
                row.Height = (short) (this.BodyLineHeight*maxLines + 100);
            }
        }

        private void Save()
        {
            this.Workbook.Write(_stream);
        }

        #endregion
    }
}

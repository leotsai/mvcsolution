using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution.Infrastructure.Utilities.NPOI
{
    public class ColumnBuilder<T>
    {
        public const int ExcelWidthRate = 37;

        private readonly Func<T, object> _property;
        public Func<T, object> Property
        {
            get { return _property; }
        }

        private int _columnWidth;
        public int ColumnWidth
        {
            get { return _columnWidth; }
        }

        private Func<int, int> _widthFunction;
        public Func<int, int> WidthFunction
        {
            get { return _widthFunction; }
        }

        private string _columnTitle;
        public string ColumnTitle
        {
            get { return _columnTitle; }
        }

        private Func<T, string[]> _cellLines;
        public Func<T, string[]> CellLines
        {
            get { return _cellLines; }
        }

        public ColumnBuilder(Func<T, object> property)
        {
            _property = property;
        }

        /// <summary>
        /// Set width of the column in px.
        /// </summary>
        /// <param name="width">in px</param>
        /// <returns></returns>
        public ColumnBuilder<T> Width(int width)
        {
            _columnWidth = width * ExcelWidthRate;
            return this;
        }

        public ColumnBuilder<T> Width(Func<int, int> widthFunction)
        {
            _widthFunction = widthFunction;
            return this;
        }

        public ColumnBuilder<T> Title(string title)
        {
            _columnTitle = title;
            return this;
        }

        public ColumnBuilder<T> Lines(Func<T, string[]> cellValues)
        {
            _cellLines = cellValues;
            return this;
        }
    }
}

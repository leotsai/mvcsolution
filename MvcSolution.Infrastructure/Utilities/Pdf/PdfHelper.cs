using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Drawing;
using Winnovative.PdfCreator;


namespace MvcSolution.Infrastructure.Utilities.Pdf
{
    public class PdfHelper
    {
        private static string LicenseKey
        {
            get { return ConfigurationManager.AppSettings["WinnovativeLicenseKey"]; }
        }

        public static void FillTemplate(string path, Stream outputStream, Dictionary<string, string> fields)
        {
            var pdfReader = new iTextSharp.text.pdf.PdfReader(path);
            var pdfStamper = new iTextSharp.text.pdf.PdfStamper(pdfReader, outputStream);
            var pdfFormFields = pdfStamper.AcroFields;

            foreach (var field in fields)
            {
                pdfFormFields.SetField(field.Key, field.Value);
            }

            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }

        public static void WriteHtmlToPdf(string html, Stream outputStream)
        {
            LicensingManager.LicenseKey = LicenseKey;
            var document = new Document();

            document.CompressionLevel = CompressionLevel.NormalCompression;
            document.Margins = new Margins();
            document.Security.CanPrint = true;
            document.Security.UserPassword = "";

            document.DocumentInformation.Author = "MvcSolution";
            document.ViewerPreferences.HideToolbar = false;

            var page = document.Pages.AddNewPage(PageSize.A4, new Margins(25, 25, 25, 25), PageOrientation.Portrait);

            var smallfont = document.Fonts.Add(new Font(new FontFamily("Verdana"), 9, GraphicsUnit.Point));

            document.FooterTemplate = document.AddTemplate(document.Pages[0].ClientRectangle.Width, 60);

            document.FooterTemplate.AddElement(new TextElement(document.FooterTemplate.ClientRectangle.Width - 75, 40,
                 "Page &p; of &P;", smallfont));

            var htmlDoc = new HtmlToPdfElement(0, 0, -1, html, string.Empty, 700);

            htmlDoc.FitWidth = true;
            htmlDoc.EmbedFonts = true;
            htmlDoc.LiveUrlsEnabled = false;

            htmlDoc.ScriptsEnabled = false;
            htmlDoc.ActiveXEnabled = false;

            // add the HTML to PDF converter element to the page

            page.AddElement(htmlDoc);
            document.Save(outputStream);
        }

        public static void WriteHtmlToPdf(Uri uri, Stream outputStream)
        {
            var client = new WebClient();
            var html = client.DownloadString(uri);
            WriteHtmlToPdf(html, outputStream);
            client.Dispose();
        }
    }
}

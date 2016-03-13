using System;
using System.Drawing.Imaging;
using MvcSolution.Data;

namespace MvcSolution.Web.Images
{
    public class ImageParameter
    {
        public int ImageType { get; set; }
        public int YearMonth { get; set; }
        public string Id { get; set; }
        public ImageSize Size { get; set; }
        public string Format { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public Guid Guid { get; set; }

        public ImageParameter GetFixed(IImageParameterFixer fixer)
        {
            return fixer.Fix(this);
        }

        public string GetRelativePath()
        {
            var folder = AppContext.GetSizedImageFolder(this.Guid);
            if (this.ImageFormat == ImageFormat.Gif)
            {
                return folder + "Full.gif";
            }
            return folder + $"{this.Size}.{this.Format}";
        }
    }
}

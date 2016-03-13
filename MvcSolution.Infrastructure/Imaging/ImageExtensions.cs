using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MvcSolution
{
    public static class ImageExtensions
    {
        public static Image ToSize(this Image image, Size size)
        {
            if (image.Width <= size.Width && image.Height <= size.Height)
            {
                return new Bitmap(image);
            }

            var newSize = image.Size.ResizeTo(size);

            var bitmap = new Bitmap(newSize.Width, newSize.Height);
            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            g.Dispose();
            return bitmap;
        }

        public static void SaveInFormat(this Image image, string path)
        {
            image.Save(path, Path.GetExtension(path).GetImageFormat());
        }
        
        public static void SaveToFileInQuality(this Image image, string path, ImageFormat format)
        {
            var parameters = new EncoderParameters();
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, new long[] { 90 });
            var encoder = ImageCodecInfo.GetImageEncoders().First(x => x.FormatID == format.Guid);
            image.Save(path, encoder, parameters);
        }

        public static Image CropCenter(this Image image, int width)
        {
            Size newSize;
            if (image.Size.Height > image.Size.Width)
            {
                newSize = new Size(width, width * image.Size.Height / image.Size.Width);
            }
            else
            {
                newSize = new Size(width * image.Size.Width / image.Size.Height, width);
            }
            var bitmap = new Bitmap(width, width);

            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            g.DrawImage(image, (width - newSize.Width) / 2, (width - newSize.Height) / 2, newSize.Width, newSize.Height);
            g.Dispose();
            return bitmap;
        }
    }
}

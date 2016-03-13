using System.Drawing.Imaging;

namespace MvcSolution
{
    public static class ImageFormatExtensions
    {
        public static ImageFormat GetImageFormat(this string extension)
        {
            switch (extension.ToLower())
            {
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".ico":
                    return ImageFormat.Icon;
                case ".bmp":
                    return ImageFormat.Bmp;
            }
            return ImageFormat.Jpeg;
        }


        public static string GetExtension(this ImageFormat format)
        {
            if (format == ImageFormat.Png)
            {
                return ".png";
            }
            if (format == ImageFormat.Bmp)
            {
                return ".bmp";
            }
            if (format == ImageFormat.Gif)
            {
                return ".gif";
            }
            return ".jpeg";
        }
    }
}

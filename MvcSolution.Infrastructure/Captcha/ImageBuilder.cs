using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MvcSolution.Infrastructure.Captcha
{
    internal class ImageBuilder
    {
        private const int Width = 120;
        private const int Height = 50;

        private const double xAmp = 1.8;
        private const double yAmp = 1.6;
        private const double xFreq = 0.07;
        private const double yFreq = 0.05;


        private readonly FontFamily[] _fonts = {
                                        new FontFamily("Times New Roman"),
                                        new FontFamily("Georgia"),
                                        new FontFamily("Arial"),
                                        new FontFamily("Comic Sans MS"), 
                                      };
        /// <summary>
        /// Creating an image for a Captcha.
        /// </summary>
        /// <param name="captchaText">Text Captcha.</param>
        /// <returns></returns>
        public Bitmap Generate(string captchaText)
        {
            var bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                var rect = new Rectangle(0, 0, Width, Height);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (var solidBrush = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(solidBrush, rect);
                }

                //Randomly choose the font name.
                var family = _fonts[new Random().Next(0, _fonts.Length)];
                var font = new Font(family, 30);

                using (var fontFormat = new StringFormat())
                {
                    //Format the font in the center.
                    fontFormat.Alignment = StringAlignment.Center;
                    fontFormat.LineAlignment = StringAlignment.Center;

                    var path = new GraphicsPath();
                    path.AddString(captchaText, font.FontFamily, (int)font.Style, font.Size, rect, fontFormat);
                    using (var solidBrush = new SolidBrush(Color.DimGray))
                    {
                        graphics.FillPath(solidBrush, DeformPath(path));
                    }

                }
                font.Dispose();

            }
            return bmp;
        }


        private GraphicsPath DeformPath(GraphicsPath graphicsPath)
        {
            var deformed = new PointF[graphicsPath.PathPoints.Length];
            var rng = new Random();
            var xSeed = rng.NextDouble()*2*Math.PI;
            var ySeed = rng.NextDouble()*2*Math.PI;
            for (int i = 0; i < graphicsPath.PathPoints.Length; i++)
            {
                var original = graphicsPath.PathPoints[i];
                var val = xFreq*original.X*yFreq*original.Y;
                var xOffset = (int) (xAmp*Math.Sin(val + xSeed));
                var yOffset = (int) (yAmp*Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, graphicsPath.PathTypes);
        }
    }
}

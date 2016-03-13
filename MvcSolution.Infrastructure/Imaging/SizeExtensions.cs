using System;
using System.Drawing;

namespace MvcSolution
{
    public static class SizeExtensions
    {
        public static Size ResizeTo(this Size origin, Size target)
        {
            if (target.Width == 0 && target.Height == 0)
            {
                return origin;
            }
            if (target.Width == 0)
            {
                var rate = (float) origin.Height/target.Height;
                if (rate <= 1)
                {
                    return new Size(origin.Width, origin.Height);
                }
                return new Size((int) (origin.Width/rate), target.Height);
            }

            if (target.Height == 0)
            {
                var rate = (float) origin.Width/target.Width;
                if (rate <= 1)
                {
                    return new Size(origin.Width, origin.Height);
                }
                return new Size(target.Width, (int) (origin.Height/rate));
            }

            var size = new Size();
            var r = Math.Max(((float) origin.Width/target.Width), ((float) origin.Height)/target.Height);
            if (r <= 1)
            {
                return new Size(origin.Width, origin.Height);
            }
            size.Width = (int) Math.Round(origin.Width/r);
            size.Height = (int) Math.Round(origin.Height/r);
            if (size.Width == 0)
            {
                size.Width = 1;
            }
            if (size.Height == 0)
            {
                size.Height = 1;
            }
            return size;
        }
    }
}

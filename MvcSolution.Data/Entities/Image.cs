using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Imaging;

namespace MvcSolution.Data
{
    public class Image : EntityBase
    {
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string OriginalPath { get; set; }
        public ImageType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }

        [Index]
        public string Key { get; set; }

        public virtual User User { get; set; }

        public static string GenerateKey(ImageType type, Guid id, ImageFormat format)
        {
            var yearMonth = DateTime.Now.ToString("yyyyMM");
            return string.Format("t{0}t{3}-{1}{2}", (int)type, id.ToString("n"), format.GetExtension(), yearMonth);
        }
    }
}

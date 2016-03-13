using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public class ImageService : ServiceBase, IImageService
    {
        public string GetOriginalPath(Guid id)
        {
            using (var db = base.NewDB())
            {
                return db.Images.Where(x => x.Id == id).Select(x => x.OriginalPath).FirstOrDefault();
            }
        }

        public string SaveAndReturnKey(ImageType type, HttpPostedFileBase file, Guid userId, bool cropCenter)
        {
            return this.SaveImageAndReturnKey(type, file, userId, null, cropCenter);
        }

        public string SaveAndReturnKey(ImageType type, Base64Image base64Image, Guid userId, bool cropCenter)
        {
            var stream = base64Image.GetStream();
            var key = SaveImageAndReturnKey(type, base64Image.FileName, stream, userId, null, cropCenter);
            stream.Dispose();
            return key;
        }

        public string DownloadAndReturnKey(ImageType type, string url, Guid userId, bool cropCenter)
        {
            using (var client = new WebClient())
            {
                var stream = client.OpenRead(url);
                if (stream == null)
                {
                    Logger.Error("ImageService.DownloadAndReturnKey", url);
                    return null;
                }
                var fileName = userId + DateTime.Now.ToString("yyyyMMMMddhhmmss") + ".jpg";
                var key = SaveImageAndReturnKey(type, fileName, stream, userId, null, cropCenter);
                stream.Close();
                return key;
            }
        }

        private string SaveImageAndReturnKey(ImageType type, HttpPostedFileBase file, Guid? userId,  string key, bool cropCenter = false)
        {
            if (file == null || file.ContentLength == 0)
            {
                throw new Exception("上传的文件不能为空");
            }
            return SaveImageAndReturnKey(type, file.FileName, file.InputStream, userId, key, cropCenter);
        }

        private string SaveImageAndReturnKey(ImageType type, string fileName, Stream inputStream, Guid? userId, string key, bool cropCenter = false)
        {
            var ext = Path.GetExtension(fileName);
            var format = ext.GetImageFormat();
            var image = new Data.Image();
            image.Id = Guid.NewGuid();
            image.Name = fileName;
            image.UserId = userId;
            image.Type = type;
            if (string.IsNullOrEmpty(key))
            {
                image.Key = Data.Image.GenerateKey(type, image.Id, format);
            }
            var folder = AppContext.GetImageOriginalFolder(userId);
            var physicalFolder = AppContext.RootFolder + folder.Replace("/", "\\");
            IoHelper.CreateDirectoryIfNotExists(physicalFolder);

            image.OriginalPath = folder + image.Id + ext;
            var physicalPath = physicalFolder + image.Id + ext;
            try
            {
                using (var inputImage = System.Drawing.Image.FromStream(inputStream))
                {
                    this.TryRotateImage(inputImage);
                    var sizedImage = cropCenter ? inputImage.CropCenter(500) : inputImage.ToSize(new Size(1024, 768));
                    using (sizedImage)
                    {
                        sizedImage.SaveToFileInQuality(physicalPath, format);
                        image.Width = sizedImage.Width;
                        image.Height = sizedImage.Height;
                    }
                }
            }
            catch
            {
                throw new KnownException("Invalid image file");
            }
            if (image.Width == 0 || image.Height == 0)
            {
                throw new KnownException("Invalid image file");
            }
            using (var db = base.NewDB())
            {
                db.Images.Add(image);
                db.SaveChanges();
            }
            return image.Key;
        }

        private void TryRotateImage(System.Drawing.Image inputImage)
        {
            try
            {
                if (inputImage.PropertyIdList.Contains(0x0112))
                {
                    var pro = inputImage.GetPropertyItem(0x0112);
                    if (pro != null && pro.Value[0] == 6 && pro.Value[1] == 0)
                    {
                        inputImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ImageService.TryRotateImage", ex);
            }
        }

    }
}

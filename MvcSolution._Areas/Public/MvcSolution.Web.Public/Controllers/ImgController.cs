using System.Web.Mvc;
using MvcSolution.Data;
using MvcSolution.Services;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Public.Controllers
{
    [MvcAuthorize]
    public class ImgController : PublicControllerBase
    {
        [HttpPost]
        public StandardJsonResult<string> Upload(ImageType type, Base64Image base64Image, bool cropCenter = false)
        {
            var result = new StandardJsonResult<string>();
            result.Try(() =>
            {
                var service = Ioc.Get<IImageService>();
                if (base64Image == null)
                {
                    if (Request.Files.Count == 0 || Request.Files[0] == null)
                    {
                        throw new KnownException("找不到文件");
                    }
                    var file = Request.Files[0];
                    result.Value = service.SaveAndReturnKey(type, file, GetUserId(), cropCenter);
                }
                else
                {
                    result.Value = service.SaveAndReturnKey(type, base64Image, GetUserId(), cropCenter);
                }
            });
            result.ContentType = "text/plain";
            return result;
        }
        
    }
}

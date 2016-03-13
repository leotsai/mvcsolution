using System.Web.Mvc;
using MvcSolution.Data;
using MvcSolution.Services;

namespace MvcSolution.Web.Admin.Controllers
{
    public class BaiduController : AdminControllerBase
    {
        public ActionResult Upload()
        {
            if (Request.QueryString["action"].Eq("config"))
            {
                return BaiduConfig();
            }

            if (Request.Files.Count == 0 || Request.Files[0] == null)
            {
                return BaiduError("找不到文件");
            }
            var file = Request.Files[0];
            var service = Ioc.Get<IImageService>();
            var key = service.SaveAndReturnKey(ImageType.UserAvatar, file, GetUserId(), false);


            return Json(new
            {
                state = "SUCCESS",
                url = "/img/full/" + key,
                title = file.FileName,
                original = file.FileName,
                error = ""
            }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        private ActionResult BaiduConfig()
        {
            var text = System.IO.File.ReadAllText(Server.MapPath("~/_storage/js/ueditor/net/config.json"));
            return new ContentResult()
            {
                Content = text,
                ContentType = "text/plain"
            };
        }

        private ActionResult BaiduError(string error)
        {
            return Json(new
            {
                error
            }, "text/plain", JsonRequestBehavior.AllowGet);
        }
    }
}

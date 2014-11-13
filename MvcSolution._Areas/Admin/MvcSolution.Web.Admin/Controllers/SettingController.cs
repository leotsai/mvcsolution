using System.Web.Mvc;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Services;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class SettingController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new SettingIndexViewModel().BuildSettings();
            return AreaView("setting/index.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult Update(string key, string value)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<Services.Admin.ISettingService>();
                service.Update(key, value);
            });
            return result;
        }

        public ActionResult Refresh()
        {
            SettingContext.Instance.Refresh();
            return RedirectToAction("index");
        }
    }
}

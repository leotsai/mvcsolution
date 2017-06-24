using System;
using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Public.Controllers
{
    public class HomeController : PublicControllerBase
    {
        public ActionResult Index()
        {
            return AreaView("home/index.cshtml", new LayoutViewModel());
        }

        public ActionResult Doc()
        {
            return AreaView("home/Doc.cshtml", new LayoutViewModel());
        }

        public ActionResult Source()
        {
            return AreaView("home/Source.cshtml", new LayoutViewModel());
        }

        public ActionResult Contact()
        {
            return AreaView("home/Contact.cshtml", new LayoutViewModel());
        }

        public ActionResult Log()
        {
            LogHelper.TryLog("home-log", "阿克大厦卡萨丁卡萨丁，暗杀神大，啊实打实大拉圣诞快乐啊，阿萨斯柯达速度快八十多，啊实打实大咖快睡吧");
            return new ContentResult(){Content = "ok"};
        }
        
        public ActionResult Loge()
        {
            try
            {
                var i = int.Parse("abc");
            }
            catch (Exception ex)
            {
                LogHelper.TryLog("home-log-ex", ex);
            }
            return new ContentResult() { Content = "ok" };
        }
    }
}

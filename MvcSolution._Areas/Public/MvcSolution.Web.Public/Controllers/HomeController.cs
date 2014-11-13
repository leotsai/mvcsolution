using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Public.Controllers
{
    public class HomeController : PublicControllerBase
    {
        public ActionResult Index()
        {
            var model = new LayoutViewModel();
            return AreaView("home/index.cshtml", model);
        }
    }
}

using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new LayoutViewModel();
            model.Title = "Admin Home";
            return AreaView("home/index",model);
        }

    }
}

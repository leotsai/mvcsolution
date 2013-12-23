using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Api.Controllers
{
    public class HomeController : ApiControllerBase
    {
        public ActionResult Index()
        {
            var model = new LayoutViewModel();
            return View(model);
        }

    }
}

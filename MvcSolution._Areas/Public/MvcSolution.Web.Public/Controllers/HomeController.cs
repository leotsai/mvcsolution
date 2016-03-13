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
    }
}

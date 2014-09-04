using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return AreaView("home/index.cshtml", new LayoutViewModel());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSolution.Web.Controllers;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Main.Controllers
{
    public class HomeController : MvcSolutionControllerBase
    {
        public ActionResult Index()
        {
            var model = new LayoutViewModel();
            return View(model);
        }

    }
}

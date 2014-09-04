using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Admin.Controllers
{
    [Authorize( Roles = Role.Names.SuperAdmin)]
    public class AdminControllerBase : Controller
    {
        /// <summary>
        /// viewPath: e.g. home/index.cshtml
        /// </summary>
        /// <param name="viewPath">e.g. home/index.cshtml</param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected ViewResult AreaView(string viewPath, object model = null)
        {
            return View("~/views/" + viewPath, model);
        }

        /// <summary>
        /// viewPath: e.g. home/index.cshtml
        /// </summary>
        /// <param name="viewPath">e.g. home/index.cshtml</param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected PartialViewResult PartialAreaView(string viewPath, object model = null)
        {
            return PartialView("~/views/" + viewPath, model);
        }

        protected MvcSolutionSession GetSession()
        {
            return System.Web.HttpContext.Current.Session.GetMvcSolutionSession();
        }
    }
}

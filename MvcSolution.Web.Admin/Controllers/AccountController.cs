using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSolution.Web.Admin.Controllers
{
    [AllowAnonymous]
    public class AccountController : AdminControllerBase
    {
        public ActionResult Login()
        {
            return View();
        }

    }
}

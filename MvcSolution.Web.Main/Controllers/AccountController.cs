using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure.Mvc;
using MVCSolution.Services.Users;
using MvcSolution.Web.Main.ViewModels;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Main.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var model = new AccountLoginViewModel();
            return View(model);
        }

        public ActionResult Register()
        {
            var model = new LayoutViewModel<User>();
            model.Model = new User();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = Ioc.GetService<IUserService>();
            if (service.CanLogin(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return Redirect("/");
            }
            model.Error = "invalid username/password.";
            return View(model);
        }

        [HttpPost]
        public StandardJsonResult Register(User model)
        {
            var result = new StandardJsonResult();
            result.Try(() => Ioc.GetService<IUserService>().Register(model));
            return result;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}

using System.Web.Mvc;
using System.Web.Security;
using MvcSolution.Services.Users;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    [AllowAnonymous]
    public class AccountController : AdminControllerBase
    {
        public ActionResult Login()
        {
            var model = new AccountLoginViewModel();
            return AreaView("account/login.cshtml", model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AreaView("account/login.cshtml", model);
            }
            var service = Ioc.GetService<IUserService>();
            if (service.CanLogin(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                GetSession().Login(model.Username);
                return RedirectToAction("index", "home");
            }
            model.Error = "invalid username/password.";
            return AreaView("account/login.cshtml", model);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            GetSession().Logout();
            return RedirectToAction("Login");
        }
    }
}

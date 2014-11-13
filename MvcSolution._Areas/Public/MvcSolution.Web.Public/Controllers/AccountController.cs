using System.Web.Mvc;
using System.Web.Security;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Web.Public.ViewModels;

namespace MvcSolution.Web.Public.Controllers
{
    public class AccountController : PublicControllerBase
    {
        public ActionResult Login()
        {
            var model = new AccountLoginViewModel();
            return AreaView("account/login.cshtml", model);
        }

        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return AreaView("account/register.cshtml", model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = Ioc.GetService<Services.IUserService>();
            if (service.CanLogin(model.Username, model.Password))
            {
                Response.SetAuthCookie(model.Username, model.RememberMe);
                GetSession().Login(model.Username);
                return Redirect("/");
            }
            model.Error = "invalid username/password.";
            return AreaView("account/login.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult Register(User user)
        {
            var result = new StandardJsonResult();
            result.Try(() => Ioc.GetService<Services.Public.IUserService>().Register(user));
            return result;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            GetSession().Logout();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}

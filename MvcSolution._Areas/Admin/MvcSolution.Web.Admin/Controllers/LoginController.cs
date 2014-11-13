using System.Web.Mvc;
using MvcSolution.Infrastructure;
using MvcSolution.Services;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return AreaView("login/index.cshtml", model);
        }

        [HttpPost, CaptchaVerify]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Error = "invalid form data";
                return AreaView("login/index.cshtml", model);
            }
            var service = Ioc.GetService<IUserService>();
            if (service.CanLogin(model.Username, model.Password))
            {
                Response.SetAuthCookie(model.Username, model.RememberMe);
                GetSession().Login(model.Username);
                return RedirectToAction("index", "home");
            }
            model.Error = "invalid username/password.";
            return AreaView("login/index.cshtml", model);
        }

    }
}
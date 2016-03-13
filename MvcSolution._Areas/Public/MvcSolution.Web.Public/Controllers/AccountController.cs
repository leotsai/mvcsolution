using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using MvcSolution.Data;
using MvcSolution.Services;
using MvcSolution.Web.Security;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Public.Controllers
{
    public class AccountController : PublicControllerBase
    {
        public ActionResult Register()
        {
            var model = new LayoutViewModel();
            return AreaView("account/registerStep1.cshtml", model);
        }

        public ActionResult Login(string returnUrl)
        {
            var model = new LayoutViewModel<string>();
            model.Model = returnUrl;
            return AreaView("account/login.cshtml", model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            GetSession().Logout();
            Session.Abandon();
            return Redirect("/login");
        }

        [HttpPost]
        public StandardJsonResult Login(string username, string password)
        {
            return base.Try(() =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new KnownException("Please enter username and password");
                }
                var service = Ioc.Get<IUserService>();
                service.Login(username, password);
                var user = service.Get(username);
                base.LoginUser(user.Id);
            });
        }

        [MvcAuthorize]
        public ActionResult RegisterCompleted()
        {
            var model = new LayoutViewModel<User>();
            var service = Ioc.Get<IUserService>();
            model.Model = service.Get(GetUserId());
            return AreaView("account/RegisterStep2.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult Register(string username, string password, bool? registerAsAdmin)
        {
            return base.Try(() =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new KnownException("Please enter username and password");
                }
                if (username.IsEmail() == false)
                {
                    throw new KnownException("The userame field should an email address");
                }
                var service = Ioc.Get<IUserService>();
                service.Register(username, password, registerAsAdmin ?? false);
                var user = service.Get(username);
                base.LoginUser(user.Id);
            });
        }

        [HttpPost]
        public StandardJsonResult CompleteRegistration(User user)
        {
            return base.Try(() =>
            {
                if (string.IsNullOrWhiteSpace(user.NickName))
                {
                    throw new KnownException("nickname is required");
                }
                var service = Ioc.Get<IUserService>();
                service.CompleteRegistration(GetUserId(), user);
            });
        }
    }
}

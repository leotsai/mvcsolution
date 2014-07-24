using System;
using System.Web.Mvc;
using MvcSolution.Infrastructure;
using MvcSolution.Infrastructure.Mvc;
using MVCSolution.Services.Users;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [AllowCrossDomainPost]
        public StandardJsonResult Login(string username, string password)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IUserService>();
                if (!service.CanLogin(username, password))
                {
                    throw new KnownException("invalid username/password.");
                }
            });
            return result;
        }
    }
}

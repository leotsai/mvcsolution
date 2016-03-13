using System.Web.Mvc;
using MvcSolution.Services;

namespace MvcSolution.Web.Public
{
    public class AppAreaRegistration : AreaRegistration
    {
        public const string Name = "Public";

        public override string AreaName => Name;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            Ioc.RegisterInheritedTypes(typeof(Services.Public.IUserService).Assembly, typeof(ServiceBase));

            var ns = new[] {"MvcSolution.Web.Public.Controllers.*"};

            //context.Map("p/{urlKey}", "article", "details", ns);
            context.Map("login", "account", "login", ns);
            context.Map("logout", "account", "logout", ns);
            context.Map("register", "account", "register", ns);
            context.Map("main/{controller}/{action}/{id}", new { controller = "main", action = "index", id = UrlParameter.Optional }, ns);
        }
    }
}

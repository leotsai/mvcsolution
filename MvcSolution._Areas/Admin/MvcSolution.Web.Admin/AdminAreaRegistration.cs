using System.Web.Mvc;
using MvcSolution.Services;

namespace MvcSolution.Web.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            Ioc.RegisterInheritedTypes(typeof(Services.Admin.IUserService).Assembly, typeof(ServiceBase));

            var ns = new[] { "MvcSolution.Web.Admin.Controllers.*" };

            context.Map("admin", "user", "index", ns);
            context.Map("admin/{controller}/{action}/{id}", new { controller = "home", action = "index", id = UrlParameter.Optional }, ns);
        }
    }
}

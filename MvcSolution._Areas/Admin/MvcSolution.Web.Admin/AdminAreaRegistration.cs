using System.Web.Mvc;
using MvcSolution.Services;

namespace MvcSolution.Web.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public const string Name = "Admin";

        public override string AreaName
        {
            get { return Name; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterIoc();
            var ns = new[] { "MvcSolution.Web.Admin.Controllers.*" };
            var defaults = new {controller = "home", action = "index", id = UrlParameter.Optional};

            context.Map("admin/{controller}/{action}/{id}", defaults, ns);
        }

        public static void RegisterIoc()
        {
            Ioc.RegisterInheritedTypes(typeof (Services.Admin.ISettingService).Assembly, typeof (ServiceBase));
        }
    }
}
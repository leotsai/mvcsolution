using System.Web.Mvc;
using MvcSolution.Services;

namespace MvcSolution.Web.Public
{
    public class PublicAreaRegistration : AreaRegistration
    {
        public const string Name = "Public";

        public override string AreaName
        {
            get { return Name; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterIoc();
            var ns = new[] { "MvcSolution.Web.Public.Controllers.*" };

            context.Map("logout", "account", "logout", ns);
            context.Map("login", "account", "login", ns);
            context.Map("register", "account", "register", ns);
        }

        public static void RegisterIoc()
        {
            Ioc.RegisterInheritedTypes(typeof (Services.Public.IUserService).Assembly, typeof (ServiceBase));
        }
    }
}
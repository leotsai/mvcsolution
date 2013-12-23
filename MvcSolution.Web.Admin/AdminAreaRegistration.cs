using System.Web.Mvc;

namespace MvcSolution.Web.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public const string Name = "Admin";

        public override string AreaName
        {
            get
            {
                return Name;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="home", action = "Index", id = UrlParameter.Optional },
                new[] { "MvcSolution.Web.Admin.Controllers.*" }
            );
        }
    }
}

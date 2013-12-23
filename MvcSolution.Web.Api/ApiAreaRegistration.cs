using System.Web.Mvc;

namespace MvcSolution.Web.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public const string Name = "Api";

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
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "MvcSolution.Web.Api.Controllers.*" }
            );
        }
    }
}

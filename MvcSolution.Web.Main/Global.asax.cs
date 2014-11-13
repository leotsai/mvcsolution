using System.Web.Mvc;
using System.Web.Routing;
using MvcSolution.Services;

namespace MvcSolution.Web.Main
{
    public class MvcSolutionApplication : MvcApplication
    {
        protected override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Ioc.RegisterInheritedTypes(typeof(ServiceBase).Assembly, typeof(ServiceBase));

            SettingContext.Instance.Init();
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            var defaults = new { controller = "home", action = "index", id = UrlParameter.Optional };
            var ns = new[] { "MvcSolution.Web.Public.Controllers.*" };

            routes.Map("{controller}/{action}/{id}", defaults, ns);

        }
    }
}
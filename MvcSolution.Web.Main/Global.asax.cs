using System.Web.Mvc;
using System.Web.Routing;
using MvcSolution.Services;

namespace MvcSolution.Web.Main
{
    public class MainApplication : MvcApplication
    {
        protected override void OnApplicationStart()
        {
            SettingContext.Instance.Init();
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            var imgns = new[] { "MvcSolution.Web.Controllers.*" };
            routes.Map("img/{size}/default-{name}.{format}", "image", "SystemDefault", imgns);
            routes.Map("img/{size}/t{imageType}t{yearMonth}-{id}.{format}", "image", "index", imgns);
            routes.Map("img/{size}/{parameter}", "image", "index", imgns);

            var ns = new[] { "MvcSolution.Web.Public.Controllers.*" };
            var defaults = new { controller = "Home", action = "Index", id = UrlParameter.Optional };

            routes.Map("", defaults, ns);
            routes.Map("{controler}/{action}/{id}", defaults, ns);
        }

        protected override void RegisterIoc()
        {
            Ioc.RegisterInheritedTypes(typeof(ServiceBase).Assembly, typeof(ServiceBase));
        }
    }
}
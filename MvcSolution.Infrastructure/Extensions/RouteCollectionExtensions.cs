using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSolution
{
    public static class RouteCollectionExtensions
    {
        public static Route Map(this RouteCollection routes, string url, string controller,
            string action)
        {
            return routes.MapRoute(url, url, new { controller, action });
        }

        public static Route Map(this RouteCollection routes, string url, string controller,
            string action, string[] namespaces)
        {
            return routes.MapRoute(url, url, new { controller, action }, namespaces);
        }

        public static Route Map(this RouteCollection routes, string url, object defaults)
        {
            return routes.MapRoute(url, url, defaults);
        }

        public static Route Map(this RouteCollection routes, string url, object defaults, string[] namespaces)
        {
            return routes.MapRoute(url, url, defaults, namespaces);
        }

        public static Route Map(this AreaRegistrationContext routes, string url, string controller,
            string action, string[] namespaces)
        {
            return routes.MapRoute(url, url, new { controller, action }, namespaces);
        }

        public static Route Map(this AreaRegistrationContext routes, string url, object defaults, string[] namespaces)
        {
            return routes.MapRoute(url, url, defaults, namespaces);
        }
    }
}

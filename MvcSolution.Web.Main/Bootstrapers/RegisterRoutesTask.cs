using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSolution.Web.Main.Bootstrapers
{
    public class RegisterRoutesTask : IBootstraperTask
    {
        #region Implementation of IBootstraperTask

        public void Execute()
        {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var ns = new[] {"MvcSolution.Web.Main.Controllers.*"};

            routes.Map("login", "account", "login", ns);
            routes.Map("register", "account", "register", ns);
            routes.Map("logout", "account", "logout", ns);


            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                ns
            );
        }

        #endregion
    }
}
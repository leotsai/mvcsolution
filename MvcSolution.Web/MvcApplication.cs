using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSolution.Web
{
    public class MvcApplication : HttpApplication
    {
        private static bool _requestIntialized = false;
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            AreaRegistration.RegisterAllAreas();
            this.RegisterRoutes(RouteTable.Routes);
            this.RegisterIoc();
            ModelBinders.Binders.Add(typeof(Base64Image), new Base64ImageBinder());
            this.OnApplicationStart();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            OnSessionStart();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (_requestIntialized == false)
            {
                _requestIntialized = true;
                OnFirstRequest();
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var culture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            OnError(exception);
        }

        #region virtual members

        protected virtual void OnApplicationStart()
        {

        }

        protected virtual void OnFirstRequest()
        {

        }

        protected virtual void OnSessionStart()
        {

        }

        protected virtual void OnError(Exception exception)
        {
            if (HttpContext.Current != null)
            {
                var httpException = exception as HttpException;
                if (httpException != null && httpException.ErrorCode == 404)
                {
                    Server.ClearError();
                    return;
                }
            }
            Logger.Error("MvcApplication.OnError", exception);
            Server.ClearError();
        }

        protected virtual void RegisterRoutes(RouteCollection routes)
        {
            
        }

        protected virtual void RegisterIoc()
        {
            
        }

        #endregion
    }
}

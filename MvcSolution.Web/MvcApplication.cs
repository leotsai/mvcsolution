using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcSolution.Web
{
    public class MvcApplication : HttpApplication
    {
        private static bool _requestIntialized = false;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
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
            var culture = new CultureInfo("en-US");
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
            Logger.Error(exception);
        }

        #endregion
    }
}

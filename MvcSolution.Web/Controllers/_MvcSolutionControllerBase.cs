using System;
using System.Web.Mvc;
using MvcSolution.Web.Security;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Controllers
{
    public abstract class MvcSolutionControllerBase : Controller
    {
        private Type _actionReturnType;
        protected abstract string AreaName { get; }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.ActionDescriptor as ReflectedActionDescriptor;
            if (action != null)
            {
                _actionReturnType = action.MethodInfo.ReturnType;
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.TryLog("MvcSolutionControllerBase.OnException", filterContext.Exception);

            if (_actionReturnType == null)
            {
                base.OnException(filterContext);
                return;
            }
            string error;
            if (filterContext.Exception is KnownException)
            {
                error = filterContext.Exception.Message;
            }
            else
            {
                error = filterContext.Exception.GetAllMessages();
            }
            if (_actionReturnType == typeof (StandardJsonResult) || (_actionReturnType == typeof(StandardJsonResult<>)))
            {
                var result = new StandardJsonResult();
                result.Fail(error);
                filterContext.Result = result;
            }
            else if (_actionReturnType == typeof (PartialViewResult))
            {
                filterContext.Result = new ContentResult() {Content = error};
            }
            else
            {
                filterContext.Result = Error(error);
            }
            filterContext.ExceptionHandled = true;
        }

        protected ViewResult AreaView(string path, object model = null)
        {
            return View("~/areas/" + AreaName + "/views/" + path, model);
        }

        protected PartialViewResult AreaPartialView(string path, object model = null)
        {
            return PartialView("~/areas/" + AreaName + "/views/" + path, model);
        }

        protected ActionResult Error(string message)
        {
            var model = new LayoutViewModel();
            model.Error = message;
            return View("~/areas/public/views/shared/error.cshtml" , model);
        }
        
        protected virtual ActionResult Message(string message)
        {
            var model = new LayoutViewModel<string>();
            model.Model = message;
            return View("~/areas/public/views/shared/message.cshtml", model);
        }

        protected MvcSession GetSession()
        {
            return System.Web.HttpContext.Current.Session.GetMvcSession();
        }

        public StandardJsonResult Try(Action action)
        {
            var result = new StandardJsonResult();
            result.Try(action);
            return result;
        }
        
        public StandardJsonResult<T> Try<T>(Func<T> action)
        {
            var result = new StandardJsonResult<T>();
            result.Try(() =>
            {
                result.Value = action();
            });
            return result;
        }

        public ActionResult TryView(Func<ActionResult> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (ex is KnownException)
                {
                    return Message(ex.Message);
                }
                return Message(ex.GetAllMessages());
            }
        }
        
        protected Guid GetUserId()
        {
            return Request.GetUserId();
        }

        protected void LoginUser(Guid userId)
        {
            Response.SetAuthCookie(userId.ToString());
            GetSession().Login(userId);
        }
    }
}

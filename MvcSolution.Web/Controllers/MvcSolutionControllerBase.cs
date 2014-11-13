using System.Web.Mvc;
using MvcSolution.Infrastructure;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Services;
using MvcSolution.Web.Security;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Controllers
{
    public abstract class MvcSolutionControllerBase : Controller
    {
        protected abstract string AreaName { get; }

        protected virtual ViewResult AreaView(string path, object model = null)
        {
            return View("~/areas/" + AreaName + "/views/" + path, model);
        }

        protected virtual PartialViewResult AreaPartialView(string path, object model = null)
        {
            return PartialView("~/areas/" + AreaName + "/views/" + path, model);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            string error;
            if (filterContext.Exception is KnownException)
            {
                error = filterContext.Exception.Message;
            }
            else
            {
                if (Request.QueryString["debug"] == SettingContext.Instance.DebugKey)
                {
                    error = filterContext.Exception.GetAllMessages();
                }
                else
                {
                    error = "服务器未知错误，请重试。如果该问题一直存在，请联系管理员。感谢您的支持。";
                }
            }
            if (Request.QueryString["ajax"] == "true")
            {
                var result = new StandardJsonResult();
                result.Fail(error);
                filterContext.Result = result;
            }
            else
            {
                var model = new LayoutViewModel();
                model.Error = error;
                filterContext.Result = this.View(this.GetErrorViewPath(), model);
            }
        }

        protected virtual string GetErrorViewPath()
        {
            return "~/Views/Shared/Error.cshtml";
        }

        protected MvcSolutionSession GetSession()
        {
            return System.Web.HttpContext.Current.Session.GetMvcSolutionSession();
        }
    }
}

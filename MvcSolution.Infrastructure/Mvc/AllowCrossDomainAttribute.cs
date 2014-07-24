using System.Web;
using System.Web.Mvc;

namespace MvcSolution.Infrastructure.Mvc
{
    public class AllowCrossDomainAttribute : ActionFilterAttribute
    {
        private readonly string _method;
        public AllowCrossDomainAttribute(string method)
        {
            _method = method;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //return;
            var method = filterContext.HttpContext.Request.HttpMethod;
            if (method.Eq("options"))
            {
                filterContext.Result = new ContentResult();
            }
            else
            {
                if (!method.Eq(_method))
                {
                    throw new HttpException(404, "Invalid request method.");
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }

    public class AllowCrossDomainGetAttribute : AllowCrossDomainAttribute
    {
        public AllowCrossDomainGetAttribute()
            : base("get")
        {
        }
    }

    public class AllowCrossDomainPostAttribute : AllowCrossDomainAttribute
    {
        public AllowCrossDomainPostAttribute()
            : base("post")
        {
        }
    }
}
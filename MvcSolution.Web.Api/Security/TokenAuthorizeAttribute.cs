using System;
using System.Linq;
using System.Web.Mvc;
using MvcSolution.Infrastructure.Mvc;

namespace MvcSolution.Web.Api.Security
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }
            if (!IsAuthorized(filterContext))
            {
                var response = filterContext.HttpContext.Response;
                var result = new StandardJsonResult();
                result.Fail("not authorized");
                result.WriteToResponse(response);
                response.End();
            }
        }

        private bool SkipAuthorization(AuthorizationContext context)
        {
            return context.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any();
        }

        public bool IsAuthorized(AuthorizationContext context)
        {
            try
            {
                var request = context.RequestContext.HttpContext.Request;
                var token = request.Headers.Get("ACCESSTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }
                var identity = TokenManager.ParseIdentity(token);
                if (identity.IsValid() == false)
                {
                    return false;
                }
                context.RequestContext.HttpContext.AddIdentity(identity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
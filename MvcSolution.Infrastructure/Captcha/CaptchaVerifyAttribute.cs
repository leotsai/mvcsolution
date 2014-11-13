using System;
using System.Web;
using System.Web.Mvc;

namespace MvcSolution.Infrastructure
{
    public class CaptchaVerifyAttribute :ActionFilterAttribute
    {
        private readonly string _textError;

        public CaptchaVerifyAttribute()
        {
            _textError = "Invalid captcha code";
        }

        public CaptchaVerifyAttribute(string textError)
        {
            _textError = textError;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            var session = HttpContext.Current.Session[Config.SessionKey];
            var valid = true;
            if (session == null)
            {
                valid = false;
            }
            else
            {
                var text = controller.ValueProvider.GetValue(Config.InputName).AttemptedValue;
                valid = text.Equals(session.ToString(), StringComparison.OrdinalIgnoreCase);
            }
            if (!valid)
            {
                controller.ModelState.AddModelError(Config.InputName, _textError);
            }
            HttpContext.Current.Session[Config.SessionKey] = null;
            base.OnActionExecuting(filterContext);
        }
    }
}

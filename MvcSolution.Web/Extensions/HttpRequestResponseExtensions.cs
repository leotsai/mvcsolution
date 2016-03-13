using System;
using System.Web;
using System.Web.Security;

namespace MvcSolution
{
    public static class HttpRequestResponseExtensions
    {
        public static Guid GetUserId(this HttpRequestBase request)
        {
            if (request.IsAuthenticated)
            {
                return HttpContext.Current.Session.GetMvcSession().User.Id;
            }
            return Guid.Empty;
        }

        public static Guid GetUserId(this HttpRequest request)
        {
            return request.RequestContext.HttpContext.Request.GetUserId();
        }

        public static void SetAuthCookie(this HttpResponseBase response, string username)
        {
            var expire = DateTime.Now.AddDays(AppContext.LoginExpireDays);
            var ticket = new FormsAuthenticationTicket(2, username, DateTime.Now, expire, true, username);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Value = FormsAuthentication.Encrypt(ticket);
            cookie.Expires = expire;
            response.Cookies.Add(cookie);

            var guestCookie = new HttpCookie("GUESTID", Guid.Empty.ToString());
            guestCookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(guestCookie);
        }
        
        public static bool IsAjax(this HttpRequest request)
        {
            return request["ajax"] == "true";
        }

        public static void ToExcel(this HttpResponseBase response, string filename)
        {
            response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            response.Charset = "UTF-8";
            response.ContentEncoding = System.Text.Encoding.Default;
            response.ContentType = "application/ms-excel";
        }
    }
}
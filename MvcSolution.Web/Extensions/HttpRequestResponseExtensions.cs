using System;
using System.Web;
using System.Web.Security;

namespace MvcSolution
{
    public static class HttpRequestResponseExtensions
    {
        public static void SetAuthCookie(this HttpResponseBase response, string username, bool remember30Days)
        {
            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(30),
                remember30Days,
                username);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            cookie.Value = FormsAuthentication.Encrypt(ticket);
            if (remember30Days)
            {
                cookie.Expires = DateTime.Now.AddDays(30);
            }
            response.Cookies.Add(cookie);
        }
    }
}
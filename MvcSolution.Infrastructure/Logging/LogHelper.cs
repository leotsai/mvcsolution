using System;
using System.Text;
using System.Threading;
using System.Web;

namespace MvcSolution
{
    public class LogHelper
    {
        private static ILogger _logger;
        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = Ioc.Get<ILogger>();
                }
                return _logger;
            }
        }

        public static void TryLog(string group, Exception exception)
        {
            try
            {
                var sb = new StringBuilder($"【{DateTime.Now.ToFullTimeString()}】{exception.GetAllMessages()}\r\n[stacktrace]: \r\n{exception.StackTrace}\r\n");
                AppendHttpRequest(sb);
                Logger.Entry(group, sb.ToString());
            }
            catch (Exception)
            {

            }
        }

        public static void TryLog(string group, string message)
        {
            try
            {
                var sb = new StringBuilder($"【{DateTime.Now.ToFullTimeString()}】{message}\r\n");
                AppendHttpRequest(sb);
                Logger.Entry(group, sb.ToString());
            }
            catch (Exception)
            {
                
            }
        }

        private static void AppendHttpRequest(StringBuilder sb)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            var request = HttpContext.Current.Request;
            sb.Append($"[{request.UserHostAddress}]-{request.HttpMethod}-{request.Url.PathAndQuery}\r\n");
            foreach (var header in request.Headers.AllKeys)
            {
                sb.Append($"{header}: {request.Headers.Get(header)}\r\n");
            }
        }
    }
}

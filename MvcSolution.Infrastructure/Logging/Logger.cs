using System;
using System.Collections.Generic;
using System.Web;

namespace MvcSolution
{
    public class Logger
    {
        private static readonly FileLogger _logger;

        static Logger()
        {
            _logger = new FileLogger();
        }

        public static void Error(string group, Exception exception)
        {
            var message = $"[Error Text]{exception.GetAllMessages()}\r\n[Error Full]{exception}";
            _logger.Log(group, message);
        }

        public static void Error(string group, string message)
        {
            var url = string.Empty;
            if (HttpContext.Current != null)
            {
                url = HttpContext.Current.Request.Url.OriginalString;
            }
            _logger.Log(group, $"【{DateTime.Now}】{url}\r\n{message}");
        }

        public static Dictionary<string, string> GetLogs()
        {
            return _logger.GetLogs();
        }
    }
}

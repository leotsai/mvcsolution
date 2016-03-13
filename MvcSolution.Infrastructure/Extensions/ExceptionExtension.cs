using System;
using System.Text;

namespace MvcSolution
{
    public static class ExceptionExtension
    {
        public static string GetAllMessages(this Exception exception)
        {
            var ex = exception;
            var sb = new StringBuilder();
            while (ex != null)
            {
                sb.AppendLine(ex.Message);
                ex = ex.InnerException;
            }
            return sb.ToString();
        }
    }
}

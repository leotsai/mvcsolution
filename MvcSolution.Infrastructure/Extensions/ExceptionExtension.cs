using System;
using System.Text;

namespace MvcSolution
{
    public static class ExceptionExtension
    {
        public static string GetAllMessages(this Exception ex)
        {
            var exception = ex;
            var sb = new StringBuilder();
            while (exception != null)
            {
                sb.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return sb.ToString();
        }
    }
}

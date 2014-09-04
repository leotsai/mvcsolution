using System.Web.Mvc;

namespace MvcSolution.Infrastructure.Mvc
{
    public abstract class MvcSolutionWebViewPage<T> : WebViewPage<T>
    {
        public override void Write(object value)
        {
            if (value is string)
            {
                WriteLiteral(value);
            }
            else
            {
                base.Write(value);
            }
        }
    }

    public abstract class MvcSolutionWebViewPage : WebViewPage
    {
        public override void Write(object value)
        {
            if (value is string)
            {
                WriteLiteral(value);
            }
            else
            {
                base.Write(value);
            }
        }
    }
}

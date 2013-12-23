using System.Text;
using System.Web.Mvc;

namespace MvcSolution.Infrastructure.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Script(this HtmlHelper helper, string script)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<script language=\"javascript\" type=\"text/javascript\">");
            sb.Append(script);
            sb.AppendLine("</script>");
            return sb.ToString();
        }

        public static string OnDocumentReady(this HtmlHelper helper, string script)
        {
            var sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function () { ");
            sb.Append(script);
            sb.AppendLine("});");
            return helper.Script(sb.ToString());
        }

        public static string SelectNav(this HtmlHelper helper, string allItemsSelector, string currentSelector, string selectedClass)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("$(\"{0}\").removeClass(\"{1}\");", allItemsSelector, selectedClass));
            sb.AppendLine(string.Format("$(\"{0}\").addClass(\"{1}\");", currentSelector, selectedClass));
            return helper.OnDocumentReady(sb.ToString());
        }

        public static string IsChecked(this HtmlHelper helper, bool isChecked)
        {
            return isChecked ? "checked = 'checked'" : string.Empty;
        }

        public static string IsSelected(this HtmlHelper helper, bool isSelected)
        {
            return isSelected ? "selected = 'selected'" : string.Empty;
        }

        public static string IsDisabled(this HtmlHelper helper, bool isDisabled)
        {
            return isDisabled ? "disabled = 'disabled'" : string.Empty;
        }

        public static string IsLastTd(this HtmlHelper helper, int current, int total)
        {
            if (current == total - 1)
                return " last";
            return "";
        }
    }
}

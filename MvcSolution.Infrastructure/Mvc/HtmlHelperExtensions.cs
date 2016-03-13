using System.Web;
using System.Web.Mvc;

namespace MvcSolution
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString IsChecked(this HtmlHelper helper, bool isChecked)
        {
            return helper.Raw(isChecked ? "checked=\"checked\"" : string.Empty);
        }

        public static IHtmlString IsSelected(this HtmlHelper helper, bool isSelected)
        {
            return helper.Raw(isSelected ? "selected=\"selected\"" : string.Empty);
        }

        public static IHtmlString IsDisabled(this HtmlHelper helper, bool isDisabled)
        {
            return helper.Raw(isDisabled ? "disabled=\"disabled\"" : string.Empty);
        }

        public static IHtmlString PagerInfo(this HtmlHelper helper, PageResult page)
        {
                  var html = "<input type=\"hidden\" class=\"total-pages\" value=\"" + page.TotalPages +
                       "\" /><input type=\"hidden\" class=\"page-index\" value=\"" + page.PageIndex + "\" />"
                       + "<input type=\"hidden\" class=\"page-rows\" value=\"" + page.TotalCount + "\" />";
            return helper.Raw(html);
        }
    }
}

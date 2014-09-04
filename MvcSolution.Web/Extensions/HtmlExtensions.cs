using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcSolution
{
    public static class HtmlExtensions
    {
        public static IHtmlString TimeFrameSelector(this HtmlHelper helper)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<select class=\"date-options\">");
            sb.AppendLine("<option value=\"Today\">今天</option>");
            sb.AppendLine("<option value=\"Yesterday\">昨天</option>");
            sb.AppendLine("<option value=\"ThisWeek\">本周</option>");
            sb.AppendLine("<option value=\"LastWeek\">上周</option>");
            sb.AppendLine("<option value=\"Last7Days\">最近7天</option>");
            sb.AppendLine("<option value=\"ThisMonth\">本月</option>");
            sb.AppendLine("<option value=\"LastMonth\">上月</option>");
            sb.AppendLine("<option value=\"Last30Days\">最近30天</option>");
            sb.AppendLine("<option value=\"Custom\">自定义</option></select>");
            return helper.Raw(sb.ToString());
        }
    }
}
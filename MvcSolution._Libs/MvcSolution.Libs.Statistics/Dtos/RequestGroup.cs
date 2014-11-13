using System.Collections.Generic;
using MvcSolution.Libs.Statistics.Enums;

namespace MvcSolution.Libs.Statistics
{
    public class RequestGroup
    {
        public string Name { get; set; }
        public List<RequestItem> Items { get; set; }

        public RequestGroup(int type)
        {
            this.Name = type.ToString();
            this.Items = new List<RequestItem>();
        }

        public RequestGroup(Platform? platform)
        {
            this.Name = platform == null ? "未知" : platform.Value.GetText();
            this.Items = new List<RequestItem>();
        }
    }
}

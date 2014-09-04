using System;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis.Entities
{
    public class RequestLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GroupHour { get; set; }
        public int GroupDay { get; set; }
        public int GroupWeek { get; set; }
        public int GroupMonth { get; set; }
        public int Type { get; set; }
        public Platform? Platform { get; set; }
        public int Value { get; set; }

        public RequestLog()
        {
            
        }

        public RequestLog(int type, Platform? platform)
        {
            var today = DateTime.Today;
            this.Date = DateTime.Now;
            this.GroupHour = DateTime.Now.ToGroupHour();
            this.GroupDay = today.ToGroupDay();
            this.GroupWeek = today.ToGroupWeek();
            this.GroupMonth = today.ToGroupMonth();
            this.Type = type;
            this.Platform = platform;
        }

        public RequestLog(int type, Platform? platform, DateTime time, int value)
        {
            this.Date = time;
            this.GroupHour = time.ToGroupHour();
            this.GroupDay = time.ToGroupDay();
            this.GroupWeek = time.ToGroupWeek();
            this.GroupMonth = time.ToGroupMonth();
            this.Type = type;
            this.Platform = platform;
            this.Value = value;
        }

        public void AddValue()
        {
            this.Value++;
            this.Date = DateTime.Now;
        }
    }
}

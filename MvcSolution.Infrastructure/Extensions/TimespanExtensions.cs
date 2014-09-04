using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution
{
    public static class TimespanExtensions
    {
        public static string GetText(this TimeSpan ts)
        {
            if (ts.TotalDays > 1)
            {
                return string.Format("{0}days{1}hrs", Math.Floor(ts.TotalDays), ts.Hours);
            }
            if (ts.TotalHours > 1)
            {
                return string.Format("{0}hrs{1}m", Math.Floor(ts.TotalHours), ts.Minutes);
            }
            if (ts.TotalMinutes > 1)
            {
                return string.Format("{0}m{1}s", Math.Floor(ts.TotalMinutes), ts.Seconds);
            }
            if (ts.TotalSeconds > 1)
            {
                return string.Format("{0}s", ts.TotalSeconds.ToString("0.000"));
            }
            return string.Format("{0}ms", ts.Milliseconds);
        }
    }
}

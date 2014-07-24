using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDate(this DateTime dateTime, int year, int month, int date)
        {
            return new DateTime(year, month, date, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static bool TimeEquals(this DateTime time, DateTime toCompare)
        {
            return time.Hour == toCompare.Hour && time.Minute == toCompare.Minute;
        }

        public static string ToStr(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return dateTime.Value.ToString();
        }

        public static string ToStr(this DateTime? dateTime, string format)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return dateTime.Value.ToString(format);
        }
    }
}

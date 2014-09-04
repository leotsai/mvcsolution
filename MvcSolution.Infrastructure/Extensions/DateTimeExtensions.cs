using System;

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


        public static string ToFullStr(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return dateTime.Value.ToString("yyyy/M/d HH:mm:ss");
        }

        public static string ToFullStr(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/M/d HH:mm:ss");
        }
    }
}

using System;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    public static class DateExtensions
    {
        public static int ToGroupDay(this DateTime date)
        {
            return date.Year * 10000 + date.Month * 100 + date.Day;
        }

        public static int ToGroupWeek(this DateTime date)
        {
            var sunday = date.ToSunday();
            return sunday.Year*10000 + sunday.Month * 100 + sunday.Day;
        }

        public static int ToGroupMonth(this DateTime date)
        {
            return date.Year*100 + date.Month;
        }

        public static int ToGroupHour(this DateTime datetime)
        {
            return datetime.Year * 1000000 + datetime.Month * 10000 + datetime.Day * 100 + datetime.Hour;
        }

        public static DateTime ToSunday(this DateTime date)
        {
            var day = date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) date.DayOfWeek;
            return date.AddDays(7 - day);
        }

        public static int ToGroupValue(this DateTime date, Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Day:
                    return date.ToGroupDay();
                case Frequency.Week:
                    return date.ToGroupWeek();
                case Frequency.Month:
                    return date.ToGroupMonth();
                case Frequency.Hour:
                default:
                    return date.ToGroupHour();
            }
        }
    }
}

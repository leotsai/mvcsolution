using System;

namespace MvcSolution.Libs.Statistics.Enums
{
    public enum Frequency
    {
        Day = 0,
        Week = 1,
        Month = 2,
        Hour = 3
    }

    public static class FrequencyHelper
    {
        public static DateTime GetNextTime(this Frequency frequency, DateTime time)
        {
            switch (frequency)
            {
                case Frequency.Hour:
                    return time.AddHours(1);
                case Frequency.Day:
                    return time.AddDays(1);
                case Frequency.Week:
                    return time.AddDays(7);
                case Frequency.Month:
                default:
                    return time.AddMonths(1);
            }
        }

        public static string GetText(this Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Hour:
                    return "小时";
                case Frequency.Day:
                    return "日";
                case Frequency.Week:
                    return "周";
                case Frequency.Month:
                default:
                    return "月";
            }
        }

        public static int CalculateItems(this Frequency frequency, DateTime min, DateTime max)
        {
            var ts = max - min;
            switch (frequency)
            {
                case Frequency.Hour:
                    return (int) ts.TotalHours;
                case Frequency.Day:
                    return (int) ts.TotalDays;
                case Frequency.Week:
                    return (int) Math.Ceiling(ts.TotalDays/7);
                case Frequency.Month:
                default:
                    return max.Year*12 + max.Month - min.Year*12 - min.Month;
            }
        }
    }
}


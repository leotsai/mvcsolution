using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Analysis.Entities;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    public class GroupedRequestDto
    {
        public int Type { get; set; }
        public int Group { get; set; }
        public int Value { get; set; }
    }

    public class GroupedPlatformRequestDto
    {
        public Platform? Platform { get; set; }
        public int Group { get; set; }
        public int Value { get; set; }
    }

    public static class GroupedRequestDtoExtensions
    {
        public static IQueryable<GroupedRequestDto> ToGroupedRequestDtos(this IQueryable<RequestLog> query,
            Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Week:
                    return query.GroupBy(x => new {x.GroupWeek, x.Type})
                        .Select(x => new GroupedRequestDto()
                        {
                            Group = x.Key.GroupWeek,
                            Type = x.Key.Type,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Month:
                    return query.GroupBy(x => new {x.GroupMonth, x.Type})
                        .Select(x => new GroupedRequestDto()
                        {
                            Group = x.Key.GroupMonth,
                            Type = x.Key.Type,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Hour:
                    return query.GroupBy(x => new { x.GroupHour, x.Type })
                        .Select(x => new GroupedRequestDto()
                        {
                            Group = x.Key.GroupHour,
                            Type = x.Key.Type,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Day:
                default:
                    return query.GroupBy(x => new {x.GroupDay, x.Type})
                        .Select(x => new GroupedRequestDto()
                        {
                            Group = x.Key.GroupDay,
                            Type = x.Key.Type,
                            Value = x.Sum(d => d.Value)
                        });
            }
        }

        public static IQueryable<GroupedPlatformRequestDto> ToGroupedPlatformRequestDtos(this IQueryable<RequestLog> query,
            Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Week:
                    return query.GroupBy(x => new { x.GroupWeek, x.Platform })
                        .Select(x => new GroupedPlatformRequestDto()
                        {
                            Group = x.Key.GroupWeek,
                            Platform = x.Key.Platform,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Month:
                    return query.GroupBy(x => new { x.GroupMonth, x.Platform })
                        .Select(x => new GroupedPlatformRequestDto()
                        {
                            Group = x.Key.GroupMonth,
                            Platform = x.Key.Platform,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Hour:
                    return query.GroupBy(x => new { x.GroupHour, x.Platform })
                        .Select(x => new GroupedPlatformRequestDto()
                        {
                            Group = x.Key.GroupHour,
                            Platform = x.Key.Platform,
                            Value = x.Sum(d => d.Value)
                        });
                case Frequency.Day:
                default:
                    return query.GroupBy(x => new { x.GroupDay, x.Platform })
                        .Select(x => new GroupedPlatformRequestDto()
                        {
                            Group = x.Key.GroupDay,
                            Platform = x.Key.Platform,
                            Value = x.Sum(d => d.Value)
                        });
            }
        }
    }
}

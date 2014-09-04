using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Analysis.Entities;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    internal static class RequestLogExtensions
    {
        public static RequestLog Get(this IQueryable<RequestLog> query, int type, Platform? platform)
        {
            var groupHour = DateTime.Now.ToGroupHour();
            return query.FirstOrDefault(x => x.Type == type && x.Platform == platform && x.GroupHour == groupHour);
        }

        public static IQueryable<RequestLog> WhereByFromDate(this IQueryable<RequestLog> query, DateTime? from)
        {
            if (from == null)
            {
                return query;
            }
            var min = from.Value.Date;
            return query.Where(x => x.Date >= min);
        }

        public static IQueryable<RequestLog> WhereByToDate(this IQueryable<RequestLog> query, DateTime? to)
        {
            if (to == null)
            {
                return query;
            }
            var max = to.Value.Date.AddDays(1);
            return query.Where(x => x.Date < max);
        }

        public static IQueryable<RequestLog> WhereByTypes(this IQueryable<RequestLog> query, int[] types)
        {
            if (types == null || types.Length == 0)
            {
                return query;
            }
            return query.Where(x => types.Contains(x.Type));
        }

        public static IQueryable<RequestLog> WhereByType(this IQueryable<RequestLog> query, int type)
        {
            return query.Where(x => x.Type == type);
        }

        public static IQueryable<RequestLog> WhereByPlatform(this IQueryable<RequestLog> query, Platform? platform)
        {
            if (platform == null)
            {
                return query;
            }
            return query.Where(x => x.Platform == platform);
        }
    }
}

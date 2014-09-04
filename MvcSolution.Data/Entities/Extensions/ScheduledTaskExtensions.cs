using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Data.Entities;

namespace MvcSolution
{
    public static class ScheduledTaskExtensions
    {
        public static IQueryable<ScheduledTask> WhereByKeyword(this IQueryable<ScheduledTask> query, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return query;
            }
            return from a in query
                where a.Name.Contains(keyword)
                      || a.Description.Contains(keyword)
                      || a.Version.Contains(keyword)
                      || a.DllFileName.Contains(keyword)
                      || a.LastWorkedVersion.Contains(keyword)
                select a;
        } 
    }
}

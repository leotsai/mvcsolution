using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSolution.Data.Entities;

namespace MvcSolution
{
    public static class BusinessBaseExtensions
    {
        public static T Get<T>(this IQueryable<T> query, int id) where T : BusinessBase
        {
            return query.FirstOrDefault(x => x.Id == id);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<int> ids) where T : BusinessBase
        {
            return query.Where(x => ids.Contains(x.Id));
        }
    }
}

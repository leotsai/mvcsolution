using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data;

namespace MvcSolution
{
    public static class EntityBaseExtensions
    {
        public static T Get<T>(this IQueryable<T> query, Guid id) where T : EntityBase
        {
            return query.FirstOrDefault(x => x.Id == id);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<Guid> ids) where T : EntityBase
        {
            return query.Where(x => ids.Contains(x.Id));
        }


        public static IQueryable<SimpleEntity> ToSimpleEntities<T>(this IQueryable<T> query)
            where T : EntityBase, ISimpleEntity
        {
            return from a in query
                   select new SimpleEntity()
                   {
                       Id = a.Id,
                       Name = a.Name
                   };
        }
    }
}

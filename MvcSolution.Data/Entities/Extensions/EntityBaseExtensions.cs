using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure;

namespace MvcSolution
{
    public static class BusinessBaseExtensions
    {
        public static T Get<T>(this IQueryable<T> query, Guid id) where T : EntityBase
        {
            return query.FirstOrDefault(x => x.Id == id);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<Guid> ids) where T : EntityBase
        {
            return query.Where(x => ids.Contains(x.Id));
        }

        public static IQueryable<SimpleEntity> ToSimpleEntities<T>(this IQueryable<T> query) where T : EntityBase, ISimpleEntity
        {
            return query.Select(x => new SimpleEntity()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public static IQueryable<SimpleEntity> OrderByName(this IQueryable<SimpleEntity> query)
        {
            return query.OrderBy(x => x.Name);
        } 
    }
}

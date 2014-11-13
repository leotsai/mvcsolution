using System;
using System.Linq;
using MvcSolution.Data.Entities;

namespace MvcSolution
{
    public static class RoleExtensions
    {
        public static string[] GetUserRoleNames(this IQueryable<Role> query, Guid userId)
        {
            var query2 = from a in query
                         from b in a.UserRoleRls
                         where b.UserId == userId
                         select a.Name;
            return query2.Distinct().ToArray();
        }
    }
}

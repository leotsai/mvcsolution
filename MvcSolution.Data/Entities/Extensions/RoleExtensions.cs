using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSolution.Data.Entities;

namespace MvcSolution
{
    public static class RoleExtensions
    {
        public static IQueryable<Role> WhereByUsername(this IQueryable<Role> query, string username)
        {
            var query2 = from a in query
                         from b in a.UserInRoles
                         where b.User.Username == username
                         select a;
            return query2.Distinct();
        }

        public static string[] GetUserRoleNames(this IQueryable<Role> query, int userId)
        {
            var query2 = from a in query
                         from b in a.UserInRoles
                         where b.UserId == userId
                         select a.Name;
            return query2.Distinct().ToArray();
        }
    }
}

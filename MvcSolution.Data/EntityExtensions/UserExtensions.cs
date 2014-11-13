using System;
using System.Linq;
using MvcSolution.Data.Entities;

namespace MvcSolution
{
    public static class UserExtensions
    {
        public static User Get(this IQueryable<User> query, string username)
        {
            return query.FirstOrDefault(x => x.Username == username);
        }

        public static IQueryable<User> WhereByDepartment(this IQueryable<User> query, Guid? departmentId)
        {
            if (departmentId == null)
            {
                return query;
            }
            return from a in query
                   where a.DepartmentId == departmentId.Value
                   select a;
        }

        public static IQueryable<User> WhereByRole(this IQueryable<User> query, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return query;
            }
            return from a in query
                   from b in a.UserRoleRls
                   where b.Role.Name == role
                   select a;
        }

        public static IQueryable<User> WhereByName(this IQueryable<User> query, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return query;
            }
            return query.Where(x => x.Name.Contains(name));
        }

        public static bool Duplicates(this IQueryable<User> query, Guid? oldUserId, string username)
        {
            if (oldUserId == null)
            {
                return query.Any(x => x.Username == username);
            }
            return query.Any(x => x.Id != oldUserId && x.Username == username);
        }
    }
}

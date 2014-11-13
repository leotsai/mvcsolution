using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSolution.Services
{
    public class RoleService : ServiceBase, IRoleService
    {
        public List<string> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Roles.Select(x => x.Name).OrderBy(x => x).ToList();
            }
        }

        public List<string> GetUserDepartmentRoles(Guid userId)
        {
            using (var db = base.NewDB())
            {
                var query = from a in db.Users
                    let d = a.Department
                    where a.Id == userId
                    select d.Roles;
                var roles = query.FirstOrDefault();
                if (roles == null)
                {
                    return new List<string>();
                }
                return roles.Split(new[] {','}).ToList();
            }
        }

        public List<string> GetUserRoles(Guid userId)
        {
            using (var db = base.NewDB())
            {
                var query = from a in db.UserRoleRls
                    where a.UserId == userId
                    select a.Role.Name;
                return query.Distinct().ToList();
            }
        }
    }
}

using System;
using System.Linq;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public class SessionUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }

        public SessionUser(User user, string[] roles)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Roles = roles;
        }

        public bool IsSuperAdmin()
        {
            return this.Roles.Contains(Role.Names.SuperAdmin);
        }

        public bool IsManager()
        {
            return this.Roles.Any(x => x == Role.Names.SuperAdmin || x == Role.Names.Manager);
        }

    }
}

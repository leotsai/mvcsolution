using System;
using System.Collections.Generic;
using MvcSolution.Infrastructure;

namespace MvcSolution.Data.Entities
{
    public class User : EntityBase, ISimpleEntity
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsDisabled { get; set; }
        public Guid? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<UserRoleRL> UserRoleRls { get; set; }
        public virtual ICollection<DepartmentUserRL> DepartmentUserRls { get; set; } 

        public User()
        {
            this.UserRoleRls = new List<UserRoleRL>();
        }

        public void Update(User user)
        {
            this.Username = user.Username;
            this.Name = user.Name;
            this.IsDisabled = user.IsDisabled;
        }
    }
}

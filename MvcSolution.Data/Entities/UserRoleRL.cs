using System;

namespace MvcSolution.Data
{
    public class UserRoleRL : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public UserRoleRL()
        {
            
        }

        public UserRoleRL(Guid userId, Guid roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
            this.Id = Guid.NewGuid();
        }
    }
}

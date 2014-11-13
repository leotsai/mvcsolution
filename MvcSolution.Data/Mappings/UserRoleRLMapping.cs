using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class UserRoleRLMapping : EntityTypeConfiguration<UserRoleRL>
    {
        public UserRoleRLMapping()
        {
            this.Require(x => x.User, x => x.UserRoleRls, x => x.UserId);
            this.Require(x => x.Role, x => x.UserRoleRls, x => x.RoleId);
        }
    }
}

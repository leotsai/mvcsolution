using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class UserRoleRLMapping : EntityTypeConfiguration<UserRoleRL>
    {
        public UserRoleRLMapping()
        {
            this.HasRequired(t => t.User)
                .WithMany(x => x.UserRoleRls)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Role)
                .WithMany(x => x.UserRoleRls)
                .HasForeignKey(t => t.RoleId)
                .WillCascadeOnDelete(false);
        }
    }
}

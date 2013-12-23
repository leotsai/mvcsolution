using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.ModelMappings
{
    /// <summary>
    /// sample model mapping
    /// </summary>
    public class UserInRoleMapping : EntityTypeConfiguration<UserInRole>
    {
        public UserInRoleMapping()
        {
            //this.HasRequired(t => t.User)
            //    .WithMany()
            //    .HasForeignKey(t => t.UserId)
            //    .WillCascadeOnDelete(false);

            //this.HasRequired(t => t.Role)
            //    .WithMany()
            //    .HasForeignKey(t => t.RoleId)
            //    .WillCascadeOnDelete(false);
        }
    }
}

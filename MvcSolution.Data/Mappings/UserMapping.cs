using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            this.Property(x => x.Username).IsRequired().HasMaxLength(250);
            this.Property(x => x.Name).HasMaxLength(100);
            this.Property(x => x.Password).IsRequired().HasMaxLength(250);

            this.Optional(x => x.Department, x => x.Users, x => x.DepartmentId);
        }
    }
}

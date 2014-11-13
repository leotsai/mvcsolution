using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class RoleMapping : EntityTypeConfiguration<Role>
    {
        public RoleMapping()
        {
            this.Property(x => x.Name).IsRequired().HasMaxLength(50);
            this.Property(x => x.Description).HasMaxLength(200);
        }
    }
}

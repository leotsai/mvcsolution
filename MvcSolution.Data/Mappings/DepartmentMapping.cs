using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            this.Property(x => x.Name).IsRequired().HasMaxLength(100);
            this.Property(x => x.Roles).HasMaxLength(500);
        }
    }
}

using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Mappings
{
    public class DepartmentUserRLMapping : EntityTypeConfiguration<DepartmentUserRL>
    {
        public DepartmentUserRLMapping()
        {
            this.Require(x => x.Department, x => x.DepartmentUserRls, x => x.DepartmentId);
            this.Require(x => x.User, x => x.DepartmentUserRls, x => x.UserId);
        }
    }
}

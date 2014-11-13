using System.Collections.Generic;
using MvcSolution.Infrastructure;

namespace MvcSolution.Data.Entities
{
    public class Department : EntityBase, ISimpleEntity
    {
        public string Name { get; set; }
        public string Roles { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<DepartmentUserRL> DepartmentUserRls { get; set; } 
    }
}

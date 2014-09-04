using System;

namespace MvcSolution.Data.Entities
{
    public class DepartmentUserRL : EntityBase
    {
        public Guid DepartmentId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
    }
}

using System.Collections.Generic;

namespace MvcSolution.Data.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserRoleRL> UserRoleRls { get; set; }

        public class Names
        {
            public const string SuperAdmin = "Super Admin";
            public const string Manager = "Manager";
            public const string CustomerService = "Customer Service";
            public const string Sales = "Sales";
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSolution.Data.Entities
{
    public class Role : EntityBase
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(200)]
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

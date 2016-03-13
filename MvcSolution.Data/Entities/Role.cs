using System.Collections.Generic;
using MvcSolution;

namespace MvcSolution.Data
{
    public class Role : EntityBase, ISimpleEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserRoleRL> UserRoleRLs { get; set; } 

        public class Names
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string SaleAgent = "SaleAgent";
            public const string Dealer = "Dealer";
            public const string Customer = "Customer";
        }
    }
}

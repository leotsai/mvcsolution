using System.Collections.Generic;
using MvcSolution;

namespace MvcSolution.Data
{
    public class Tag: EntityBase, ISimpleEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserTagRL> UserTagRLs { get; set; } 
    }
}

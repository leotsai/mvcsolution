using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcSolution.Infrastructure;

namespace MvcSolution.Data.Entities
{
    public class Department : EntityBase, ISimpleEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Roles { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}

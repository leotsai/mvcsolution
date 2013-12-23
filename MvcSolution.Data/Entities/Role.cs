using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSolution.Data.Entities
{
    public class Role : BusinessBase
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }

        public Role()
        {
            UserInRoles = new List<UserInRole>();
        }
    }
}

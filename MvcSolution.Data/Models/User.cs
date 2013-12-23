using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSolution.Data.Models
{
    public class User : BusinessBase
    {
        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string Password { get; set; }


        public bool IsDisabled { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }

        public User()
        {
            UserInRoles = new List<UserInRole>();
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Entities
{
    public class User : BusinessBase
    {
        [MaxLength(20),Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "We guess you are entering a valid email address, haha.")]
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

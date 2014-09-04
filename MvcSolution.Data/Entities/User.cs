using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcSolution.Infrastructure;

namespace MvcSolution.Data.Entities
{
    public class User : EntityBase, ISimpleEntity
    {
        [MaxLength(500), Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "We guess you are entering a valid email address, haha.")]
        public string Username { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string Password { get; set; }

        public bool IsDisabled { get; set; }

        public Guid? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<UserRoleRL> UserRoleRls { get; set; }

        public User()
        {
            this.UserRoleRls = new List<UserRoleRL>();
        }

        public void Update(User user)
        {
            this.Username = user.Username;
            this.Name = user.Name;
            this.IsDisabled = user.IsDisabled;
        }
    }
}

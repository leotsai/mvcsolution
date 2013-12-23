using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution.Data.Models
{
    public class UserInRole : BusinessBase
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}

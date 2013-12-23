using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Entities
{
    public class UserInRole : BusinessBase
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}

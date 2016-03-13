using System;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public class SessionUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string NickName { get; set; }
        public string[] Roles { get; set; }

        public SessionUser(User user, string[] roles)
        {
            this.Id = user.Id;
            this.Roles = roles;
            this.NickName = user.NickName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSolution.Data.Entities;
using MvcSolution.Data.Entities;

namespace MVCSolution.Services.Users
{
    public interface IUserService
    {
        User Get(string username);
        string[] GetRoles(string username);
        bool CanLogin(string username, string password);
        void Register(User user);

        SessionUser GetSessionUser(string username);
    }
}

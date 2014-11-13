using System;
using System.Collections.Generic;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public interface IUserService
    {
        User Get(string username);
        bool CanLogin(string username, string password);
        SessionUser GetSessionUser(string username);
        User Get(Guid id);
    }
}

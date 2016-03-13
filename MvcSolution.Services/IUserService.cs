using System;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public interface IUserService
    {
        SessionUser GetSessionUser(Guid userId);
        void Login(string username, string password);
        User Get(Guid id);
        User Get(string username);
        void Register(string username, string password, bool registerAsAdmin);
        void CompleteRegistration(Guid userId, User user);
    }
}

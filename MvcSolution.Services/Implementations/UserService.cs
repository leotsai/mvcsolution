using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services
{
    public class UserService : ServiceBase, IUserService
    {
        #region Implementation of IUserService

        public User Get(string username)
        {
            using (var db = base.NewDB())
            {
                return db.Users.Get(username);
            }
        }

        public bool CanLogin(string username, string password)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.Get(username);
                return user != null && user.IsDisabled == false
                       && user.Password == CryptoService.MD5Encrypt(password);
            }
        }

        public SessionUser GetSessionUser(string username)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.Get(username);
                if (user == null)
                {
                    return null;
                }
                var roleNames = db.Roles.GetUserRoleNames(user.Id);
                return new SessionUser(user, roleNames);
            }
        }

        public User Get(Guid id)
        {
            using (var db = base.NewDB())
            {
                return db.Users.Get(id);
            }
        }

        #endregion
    }
}

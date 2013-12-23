using System;
using System.Linq;
using MvcSolution;
using MvcSolution.Data.Entities;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure.Security;

namespace MVCSolution.Services.Users
{
    public class UserService : ServiceBase<User>, IUserService
    {
        #region Implementation of IUserService

        public User Get(string username)
        {
            using(var db = base.NewDB())
            {
                return db.Users.Get(username);
            }
        }

        public string[] GetRoles(string username)
        {
            using (var db = base.NewDB())
            {
                return db.Roles.WhereByUsername(username).Select(x => x.Name).Distinct().ToArray();
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

        public void Register(User user)
        {
            using (var db = base.NewDB())
            {
                if (db.Users.Get(user.Username) != null)
                {
                    throw new Exception("username already registered.");
                }
                user.Password = CryptoService.MD5Encrypt(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        #endregion
    }
}

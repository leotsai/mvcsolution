using System;
using System.Linq;
using System.Web;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public SessionUser GetSessionUser(Guid userId)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return null;
                }
                var roleNames = db.Roles.GetUserRoleNames(user.Id);
                var session = new SessionUser(user, roleNames);
                
                return session;
            }
        }

        public void Login(string username, string password)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new KnownException("The username doesn't exists");
                }
                if (CryptoService.Md5HashEncrypt(password) != user.Password)
                {
                    throw new KnownException("Incorrect password");
                }
            }
        }

        public User Get(Guid id)
        {
            using (var db = base.NewDB())
            {
                return db.Users.Get(id);
            }
        }

        public User Get(string username)
        {
            using (var db = base.NewDB())
            {
                return db.Users.FirstOrDefault(x => x.Username == username);
            }
        }

        public void Register(string username, string password, bool registerAsAdmin)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var address = IpHelper.GetAddress(ip);
            using (var db = base.NewDB())
            {
                if (db.Users.Any(x => x.Username == username))
                {
                    throw new KnownException("username already exists");
                }
                var user = new User(username, CryptoService.Md5HashEncrypt(password));
                user.RegisterIp = ip;
                user.RegisterAddress = address;
                db.Users.Add(user);

                if (registerAsAdmin)
                {
                    var role = db.Roles.First(x => x.Name == Role.Names.SuperAdmin);
                    db.UserRoleRLs.Add(new UserRoleRL(user.Id, role.Id));
                }
                db.SaveChanges();
            }
        }

        public void CompleteRegistration(Guid userId, User user)
        {
            using (var db = base.NewDB())
            {
                var dbUser = db.Users.Get(userId);
                dbUser.UpdateByCompleteRegistration(user);
                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services.Public.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        public void Register(User user)
        {
            using (var db = base.NewDB())
            {
                if (db.Users.Get(user.Username) != null)
                {
                    throw new KnownException("username already registered.");
                }
                user.NewId();
                user.Password = CryptoService.MD5Encrypt(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}

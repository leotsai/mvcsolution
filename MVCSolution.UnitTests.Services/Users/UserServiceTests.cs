using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCSolution.Services.Users;
using MvcSolution;
using MvcSolution.Data.Context;
using MvcSolution.UnitTestBase;
using MvcSolution.UnitTestBase.Mocks;
using NUnit.Framework;

namespace MVCSolution.UnitTests.Services.Users
{
    public class UserServiceTests : DbUnitTestBase
    {
        [Test]
        public void Can_GetByUsername()
        {
            using (var db = base.NewDB())
            {
                var user = Mock.Users.New();
                user.Username = "test1232312";
                db.Users.Add(user);
                db.SaveChanges();
            }

            var service = Ioc.GetService<IUserService>();
            Assert.IsNotNull(service.Get("test1232312"));
        }
    }
}

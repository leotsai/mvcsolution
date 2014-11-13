using MvcSolution.Services.Users;
using MvcSolution.UnitTestBase;
using MvcSolution.UnitTestBase.Mocks;
using NUnit.Framework;

namespace MvcSolution.UnitTests.Services.Users
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

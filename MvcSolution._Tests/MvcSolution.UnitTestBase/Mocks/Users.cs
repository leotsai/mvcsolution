using System;
using MvcSolution.Data.Entities;

namespace MvcSolution.UnitTestBase.Mocks
{
    public partial class Mock
    {
        public class Users
        {
            public static User New()
            {
                return new User()
                    {
                        Username = "testusername",
                        Password = "testpassword",
                        Name = "test user",
                        CreatedTime = DateTime.Now
                    };
            }
        }
    }
    
}

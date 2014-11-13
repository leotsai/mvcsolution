using System;
using MvcSolution.Data.Entities;

namespace MvcSolution.UnitTestBase.Mocks
{
    public partial class Mock
    {
        public class Roles
        {
            public static Role New()
            {
                return new Role()
                    {
                        Name = "testrole",
                        CreatedTime = DateTime.Now
                    };
            }
        }
    }
    
}

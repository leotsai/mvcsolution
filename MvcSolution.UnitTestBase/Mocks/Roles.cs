using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

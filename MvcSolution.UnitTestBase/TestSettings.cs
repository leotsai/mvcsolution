using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MvcSolution.UnitTestBase
{
    public class TestSettings
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MvcSolutionDataContext"].ConnectionString; }
        }
    }
}

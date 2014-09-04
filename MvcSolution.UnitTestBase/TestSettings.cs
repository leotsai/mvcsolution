using System.Configuration;

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

using System.Configuration;
using MvcSolution.Data.Context;
using MvcSolution.Infrastructure;
using NUnit.Framework;

namespace MvcSolution.UnitTestBase
{
    public class TestSettings
    {
        public static string ConnectionString
        {
            get { return MvcSolutionDataContext.GetConnectionString(); }
        }

        [Test]
        public void Can_Generate_Password()
        {
            var pwd = CryptoService.MD5Encrypt("123456");

            var d = 0;
        }
    }
}

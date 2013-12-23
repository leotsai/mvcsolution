using MvcSolution.Web.Main.Bootstrapers;
using NUnit.Framework;
using MvcSolution.Data.Context;
using Microsoft.Practices.Unity;

namespace MvcSolution.UnitTestBase
{
    [TestFixture]
    public class UnitTestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            new Bootstraper().Run();
        }
    }
}

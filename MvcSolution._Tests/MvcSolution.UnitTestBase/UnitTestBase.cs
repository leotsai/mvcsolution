using MvcSolution.Web.Main.Bootstrapers;
using NUnit.Framework;

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

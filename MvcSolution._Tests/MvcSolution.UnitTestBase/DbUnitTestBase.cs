using MvcSolution.Data.Context;
using MvcSolution.UnitTestBase.Framework;
using NDbUnit.Core;
using NDbUnit.Core.SqlClient;
using NUnit.Framework;

namespace MvcSolution.UnitTestBase
{
    public class DbUnitTestBase : UnitTestBase, IDbUnitTest
    {
        [TestFixtureSetUp]
        public virtual void TestSetupFixture()
        {
            this.InitData();
        }

        [SetUp]
        public void Setup()
        {
            this.Database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        [TestFixtureTearDown]
        public void RestoreData()
        {
            this.Database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        [Test]
        public void InitTestData()
        {

        }

        protected MvcSolutionDataContext NewDB()
        {
            return new MvcSolutionDataContext();
        }

        #region Implementation of IDbUnitTest

        public virtual TestDataType DataType
        {
            get { return TestDataType.Initials; }
        }

        public virtual string ConnectionString
        {
            get { return TestSettings.ConnectionString; }
        }

        public SqlDbUnitTest Database { get; set; }

        public virtual string CustomDataPath
        {
            get { return null; }
        }

        #endregion
    }
}

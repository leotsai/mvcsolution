using NDbUnit.Core.SqlClient;

namespace MvcSolution.UnitTestBase.Framework
{
    public interface IDbUnitTest
    {
        TestDataType DataType { get; }
        string ConnectionString { get; }
        SqlDbUnitTest Database { get; set; }
        string CustomDataPath { get; }
    }
}



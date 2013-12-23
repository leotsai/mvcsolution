using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MvcSolution.UnitTestBase.Framework;
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



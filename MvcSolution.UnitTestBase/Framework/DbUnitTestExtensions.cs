using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MvcSolution.UnitTestBase.Framework;
using NDbUnit.Core.SqlClient;

namespace MvcSolution
{
    public static class DbUnitTestExtensions
    {
        public static void InitData(this IDbUnitTest test)
        {
            test.Database = new SqlDbUnitTest(test.ConnectionString);
            test.Database.ReadXmlSchema("MvcSolutionSchema.xsd");
            if (test.DataType == TestDataType.Empty)
            {
                test.Database.ReadXml("TestData/Empty.xml");
            }
            else if (test.DataType == TestDataType.Custom)
            {
                if (Directory.Exists(test.CustomDataPath))
                {
                    test.Database.ReadXmlFromFolder(test.CustomDataPath);
                }
                else
                {
                    test.Database.ReadXml(test.CustomDataPath);
                }
            }
            else
            {
                var folder = "TestData/" + test.DataType.ToString();
                test.Database.ReadXmlFromFolder(folder);
            }
        }
    }
}
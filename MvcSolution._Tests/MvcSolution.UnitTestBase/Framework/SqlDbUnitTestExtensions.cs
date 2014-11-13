using System.IO;
using NDbUnit.Core.SqlClient;

namespace MvcSolution
{
    public static class SqlDbUnitTestExtensions
    {
        public static void ReadXmlFromFolder(this SqlDbUnitTest test, string folderPath)
        {
            var xmls = Directory.GetFiles(folderPath);
            if (xmls.Length == 0)
            {
                return;
            }
            test.ReadXml(xmls[0]);
            for (var i = 1; i < xmls.Length; i++)
            {
                test.AppendXml(xmls[i]);
            }
        }
    }
}

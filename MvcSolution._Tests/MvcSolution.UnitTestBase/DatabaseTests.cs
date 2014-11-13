using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MvcSolution.UnitTestBase.Framework;
using NDbUnit.Core.SqlClient;
using NUnit.Framework;
using MvcSolution.Data.Context;

namespace MvcSolution.UnitTestBase
{
    public class DatabaseTests : IDbUnitTest
    {
        [Test, Explicit]
        public void Can_RecreateDatabase()
        {
            this.RecreateDatabase();
        }

        [Test, Explicit]
        public void Can_RecreateDatabase_And_InitData()
        {
            this.RecreateDatabase();
            this.InitData();
        }

        private void RecreateDatabase()
        {
            var context = new MvcSolutionDataContext();
            var sql =
                @"
USE MASTER  
DECLARE @i INT  
SELECT   @i=1  
DECLARE @sSPID VARCHAR(100)
DECLARE KILL_CUR SCROLL CURSOR FOR    
SELECT SPID FROM sysprocesses WHERE DBID=DB_ID('MvcSolutionDB')                           
OPEN KILL_CUR                  
IF @@CURSOR_ROWS=0 GOTO END_KILL_CUR  
FETCH FIRST FROM KILL_CUR INTO @sSPID              
EXEC('KILL   '+@sSPID)                
WHILE @i<@@CURSOR_ROWS  
BEGIN      
    FETCH NEXT FROM KILL_CUR INTO @sSPID              
    EXEC('KILL '+@sSPID)  
    SELECT @i=@i+1  
END  
END_KILL_CUR:  
CLOSE KILL_CUR  
DEALLOCATE KILL_CUR";
            if (context.Database.Exists())
            {
                try
                {
                    context.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception)
                {
                }
            }
            context.Database.Delete();
            context.Database.Create();

            var connection = new SqlConnection(this.ConnectionString);
            connection.Open();

            var contextType = typeof(MvcSolutionDataContext);
            var excludedTables = new string[] { };
            var additionTables = new string[] { };
            var tables = contextType.GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1" && x.CanWrite)
                .Select(p => p.Name)
                .ToList();
            tables.RemoveAll(excludedTables.Contains);
            tables.AddRange(additionTables);

            var ds = new DataSet("MvcSolutionSchema");
            ds.Namespace = "http://tempuri.org/MvcSolutionSchema.xsd";
            ds.ExtendedProperties.Add("targetNamespace", ds.Namespace);
            foreach (var table in tables)
            {
                var adapter = new SqlDataAdapter("select * from " + table, connection);
                adapter.FillSchema(ds, SchemaType.Mapped, table);
            }
            ds.WriteXmlSchema("../../MvcSolutionSchema.xsd");
            connection.Close();
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

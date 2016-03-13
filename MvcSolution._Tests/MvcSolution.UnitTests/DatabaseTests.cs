using System;
using System.Linq;
using MvcSolution;
using MvcSolution.Data;
using NUnit.Framework;

namespace MvcSolution.UnitTests
{
    public class DatabaseTests
    {
        public const string DbName = "MvcSolution";

        [Test]
        public void Can_RecreateDatabase()
        {
            var context = new MvcSolutionDbContext();
            var sql =
                @"
USE MASTER  
DECLARE @i INT  
SELECT   @i=1  
DECLARE @sSPID VARCHAR(100)
DECLARE KILL_CUR SCROLL CURSOR FOR    
SELECT SPID FROM sysprocesses WHERE DBID=DB_ID('" + DbName + @"')                           
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
        }

        [Test]
        public void Can_Init_Database()
        {
            using (var db = new MvcSolutionDbContext())
            {
                var roleNames = new[] {Role.Names.SuperAdmin, Role.Names.SaleAgent, Role.Names.Dealer, Role.Names.Customer};
                foreach (var name in roleNames)
                {
                    if (!db.Roles.Any(x => x.Name == name))
                    {
                        db.Roles.Add(new Role()
                        {
                            Id = Guid.NewGuid(),
                            Name = name
                        });
                    }
                }
                db.SaveChanges();
            }
        }
    }
}

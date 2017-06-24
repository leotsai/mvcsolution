using System;
using System.Linq;
using MvcSolution;
using MvcSolution.Data;
using NUnit.Framework;

namespace MvcSolution.UnitTests
{
    public class DatabaseTests
    {
        [Test]
        public void Can_RecreateDatabase()
        {
            using (var context = new MvcSolutionDbContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }
                context.Database.Create();
            }
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

using System;
using System.Data.Entity;
using MvcSolution.Data.Entities;
using MvcSolution.Data.Mappings;

namespace MvcSolution.Data.Context
{
    public class MvcSolutionDataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentUserRL> DepartmentUserRls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleRL> UserRoleRls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserRoleRLMapping());
        }

        public bool TrySave()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}

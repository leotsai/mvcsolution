using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Context
{
    public class MvcSolutionDataContext : DbContext
    {
        public MvcSolutionDataContext()
        {
            Database.SetInitializer<MvcSolutionDataContext>(null);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentUserRL> DepartmentUserRls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleRL> UserRoleRls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mappings = GetType().Assembly.GetInheritedTypes(typeof(EntityTypeConfiguration<>));
            foreach (var mapping in mappings)
            {
                dynamic instance = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(instance);
            }
        }

        public static MvcSolutionDataContext New()
        {
            return new MvcSolutionDataContext();
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MvcSolutionDataContext"].ConnectionString;
        }
    }
}

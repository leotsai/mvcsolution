using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Text;
using MvcSolution.Data;

namespace MvcSolution.Data
{
    public class MvcSolutionDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; } 
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleRL> UserRoleRLs { get; set; } 
        public DbSet<UserTagRL> UserTagRLs { get; set; } 

        public MvcSolutionDbContext()
        {
            Database.SetInitializer<MvcSolutionDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mappings = GetType().Assembly.GetInheritedTypes(typeof(EntityTypeConfiguration<>));
            foreach (var mapping in mappings)
            {
                dynamic instance = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(instance);
            }
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MvcSolutionDbContext"].ConnectionString;
        }


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var item in error.ValidationErrors)
                    {
                        sb.AppendLine(item.PropertyName + ": " + item.ErrorMessage);
                    }
                }
                LogHelper.TryLog("SaveChanges.DbEntityValidation", ex.GetAllMessages() + sb);
                throw;
            }
        }
    }
}

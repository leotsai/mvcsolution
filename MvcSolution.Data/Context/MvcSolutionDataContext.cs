using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MvcSolution.Data.Entities;
using MvcSolution.Data.ModelMappings;
using MvcSolution.Data.Entities;

namespace MvcSolution.Data.Context
{
    public class MvcSolutionDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new UserInRoleMapping());
        }

        #region Implementation of IUnitOfWork

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

        #endregion
    }
}

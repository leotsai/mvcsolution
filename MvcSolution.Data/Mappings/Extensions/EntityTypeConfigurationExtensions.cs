using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;
using MvcSolution.Data;

namespace MvcSolution
{
    public static class EntityTypeConfigurationExtensions
    {
        public static void Require<T, TRequired, TKey>(this EntityTypeConfiguration<T> config,
               Expression<Func<T, TRequired>> required,
               Expression<Func<TRequired, ICollection<T>>> many,
               Expression<Func<T, TKey>> key)
            where T : EntityBase
            where TRequired : EntityBase
        {
            config.HasRequired(required).WithMany(many).HasForeignKey(key).WillCascadeOnDelete(false);
        }

        public static void Optional<T, TOptional, TKey>(this EntityTypeConfiguration<T> config,
               Expression<Func<T, TOptional>> required,
               Expression<Func<TOptional, ICollection<T>>> many,
               Expression<Func<T, TKey>> key)
            where T : EntityBase
            where TOptional : EntityBase
        {
            config.HasOptional(required).WithMany(many).HasForeignKey(key).WillCascadeOnDelete(false);
        }
    }
}

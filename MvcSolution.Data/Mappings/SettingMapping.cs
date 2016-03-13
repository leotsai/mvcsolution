using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data;

namespace MvcSolution.Data.Mappings
{
    public class SettingMapping : EntityTypeConfiguration<Setting>
    {
        public SettingMapping()
        {
            this.Property(x => x.Key).IsRequired().HasMaxLength(100);
            this.Property(x => x.Notes).HasMaxLength(250);
        }
    }
}

using System.Data.Entity.ModelConfiguration;

namespace MvcSolution.Data.Entities
{
    public class SettingMapping : EntityTypeConfiguration<Setting>
    {
        public SettingMapping()
        {
            this.Property(x => x.Key).IsRequired().HasMaxLength(200);
            this.Property(x => x.Value).HasMaxLength(500);
            this.Property(x => x.Notes).HasMaxLength(500);
        }
    }
}

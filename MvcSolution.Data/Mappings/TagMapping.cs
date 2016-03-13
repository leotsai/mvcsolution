using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data;

namespace MvcSolution.Data.Mappings
{
    public class TagMapping : EntityTypeConfiguration<Tag>
    {
        public TagMapping()
        {
            this.Property(x => x.Name).IsRequired().HasMaxLength(20);
        }
    }
}

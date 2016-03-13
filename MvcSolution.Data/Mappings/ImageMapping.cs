using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data;

namespace MvcSolution.Data.Mappings
{
    public class ImageMapping : EntityTypeConfiguration<Image>
    {
        public ImageMapping()
        {
            this.Property(x => x.Key).IsRequired().HasMaxLength(200);
            this.Property(x => x.Name).IsRequired().HasMaxLength(200);
            this.Property(x => x.OriginalPath).IsRequired().HasMaxLength(200);

            this.Optional(x => x.User, x => x.Images, x => x.UserId);
        }
    }
}

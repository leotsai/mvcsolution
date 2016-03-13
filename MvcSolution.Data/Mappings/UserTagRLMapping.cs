using System.Data.Entity.ModelConfiguration;
using MvcSolution.Data;

namespace MvcSolution.Data.Mappings
{
    public class UserTagRLMapping : EntityTypeConfiguration<UserTagRL>
    {
        public UserTagRLMapping()
        {
            this.Require(x => x.User, x => x.UserTagRLs, x => x.UserId);
            this.Require(x => x.Tag, x => x.UserTagRLs, x => x.TagId);
        }
    }
}

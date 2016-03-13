using System;

namespace MvcSolution.Data
{
    public class UserTagRL: EntityBase
    {
        public Guid UserId { get; set; }
        public Guid TagId { get; set; }

        public virtual User User { get; set; }
        public virtual Tag Tag { get; set; }

        public UserTagRL()
        {
            
        }

        public UserTagRL(Guid userId, Guid tagId)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.TagId = tagId;
        }
    }
}

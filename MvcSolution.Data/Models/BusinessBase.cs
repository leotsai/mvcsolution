using System;

namespace MvcSolution.Data.Models
{
    public abstract class BusinessBase
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public bool IsNew
        {
            get
            {
                return this.Id <= 0;
            }
        }

        protected BusinessBase()
        {
            CreatedDateTime = DateTime.UtcNow;
        }

        public virtual void UpdateFrom(BusinessBase source)
        {
            this.Id = source.Id;
            this.CreatedDateTime = source.CreatedDateTime;
            this.LastUpdatedDate = source.LastUpdatedDate;
        }
    }
}

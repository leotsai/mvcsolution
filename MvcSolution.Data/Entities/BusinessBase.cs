using System;

namespace MvcSolution.Data.Entities
{
    public abstract class BusinessBase
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }

        public bool IsNew
        {
            get
            {
                return this.Id <= 0;
            }
        }

        protected BusinessBase()
        {
            CreatedTime = DateTime.UtcNow;
        }

        public virtual void UpdateFrom(BusinessBase source)
        {
            this.Id = source.Id;
            this.CreatedTime = source.CreatedTime;
            this.LastUpdatedTime = source.LastUpdatedTime;
        }
    }
}

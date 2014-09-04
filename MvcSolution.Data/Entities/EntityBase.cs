using System;

namespace MvcSolution.Data.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }

        public bool IsNew
        {
            get { return this.Id == Guid.Empty; }
        }

        protected EntityBase()
        {
            this.Id = Guid.Empty;
            CreatedTime = DateTime.Now;
        }

        public void NewId()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using MvcSolution.Data.Enums;

namespace MvcSolution.Data.Entities
{
    public class ServiceLog : EntityBase
    {
        public Guid? TaskId { get; set; }

        [Required, MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }
        public ServiceLogLevel Level { get; set; }

        public virtual ScheduledTask Task { get; set; }
    }
}

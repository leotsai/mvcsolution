using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcSolution.Data.Entities
{
    public class ScheduledTask : EntityBase
    {
        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Type { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required, MaxLength(500)]
        public string IntervalDescription { get; set; }

        [Required, MaxLength(200)]
        public string Version { get; set; }

        [MaxLength(200)]
        public string LastWorkedVersion { get; set; }

        [Required, MaxLength(200)]
        public string DllFileName { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsBusy { get; set; }
        public bool DllExists { get; set; }
        public bool RunImmediately { get; set; }

        public DateTime? StartedTime { get; set; }
        public DateTime? StoppedTime { get; set; }
        public DateTime? LastWorkStartedTime { get; set; }
        public DateTime? LastWorkCompletedTime { get; set; }

        public virtual ICollection<ServiceLog> ServiceLogs { get; set; } 

        public void UpdateOnDllChanged(ScheduledTask task)
        {
            this.Name = task.Name;
            this.Description = task.Description;
            this.IntervalDescription = task.IntervalDescription;
            this.LastUpdatedTime = DateTime.Now;
            this.Version = task.Version;
            this.DllExists = true;
            this.DllFileName = task.DllFileName;
        }
    }
}

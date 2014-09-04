using System;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services.WinServices
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IntervalDescription { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? LastWorkStartedTime { get; set; }
        public DateTime? LastWorkCompletedTime { get; set; }
        public string LastWorkDuration { get; set; }

        public TaskDto(ScheduledTask task)
        {
            this.Id = task.Id;
            this.Name = task.Name;
            this.Description = task.Description;
            this.IntervalDescription = task.IntervalDescription;
            this.LastWorkStartedTime = task.LastWorkStartedTime;
            this.LastWorkCompletedTime = task.LastWorkCompletedTime;
            this.Status = task.GetStatus();
            this.LastWorkDuration = task.GetLastWorkDuration();
        }
    }
}

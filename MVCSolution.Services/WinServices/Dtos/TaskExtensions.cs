using MvcSolution.Data.Entities;
using MvcSolution.Services.WinServices;


namespace MvcSolution
{
    public static class TaskExtensions
    {
        public static bool IsEditable(this ScheduledTask task)
        {
            if (task.IsEnabled)
            {
                return false;
            }
            if (task.StartedTime == null)
            {
                return true;
            }
            return task.StoppedTime.HasValue && task.StoppedTime.Value > task.StartedTime.Value;
        }

        public static string GetLastWorkDuration(this ScheduledTask task)
        {
            if (task.LastWorkStartedTime == null || task.LastWorkCompletedTime == null)
            {
                return "--";
            }
            if (task.LastWorkCompletedTime < task.LastWorkStartedTime)
            {
                return "--";
            }
            return (task.LastWorkCompletedTime.Value - task.LastWorkStartedTime.Value).GetText();
        }

        public static TaskStatus GetStatus(this ScheduledTask task)
        {
            if (task.IsEnabled)
            {
                if (task.StartedTime == null)
                {
                    return TaskStatus.Starting;
                }
                if (task.IsBusy)
                {
                    return TaskStatus.Busy;
                }
                if (task.StoppedTime != null && task.StartedTime.Value < task.StoppedTime.Value)
                {
                    return TaskStatus.Starting;
                }
                return TaskStatus.Running;
            }
            if (task.StartedTime == null)
            {
                return TaskStatus.Stopped;
            }
            if (task.StoppedTime == null)
            {
                return TaskStatus.Stopping;
            }
            if (task.StoppedTime.Value < task.StartedTime.Value)
            {
                return TaskStatus.Stopping;
            }
            return TaskStatus.Stopped;
        }
    }
}
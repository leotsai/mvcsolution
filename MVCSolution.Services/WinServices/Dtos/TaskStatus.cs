using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSolution.Services.WinServices
{
    public enum TaskStatus
    {
        Starting = 1,
        Running = 2,
        Busy = 3,
        Stopping = 4,
        Stopped = 5,
        AwaitExecuting = 6
    }
}

namespace MvcSolution.Services.WinServices
{
    public static class TaskStatusHelper
    {
        public static string GetText(this TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Starting:
                    return "Starting...";
                case TaskStatus.Running:
                    return "Running...";
                case TaskStatus.Busy:
                    return "Busy...";
                case TaskStatus.Stopping:
                    return "Stopping...";
                case TaskStatus.AwaitExecuting:
                    return "Await executing...";
            }
            return "Stopped";
        }

        public static TaskStatus[] GetAll()
        {
            return new []
            {
                TaskStatus.Starting,
                TaskStatus.Running,
                TaskStatus.Busy,
                TaskStatus.Stopping,
                TaskStatus.Stopped,
                TaskStatus.AwaitExecuting
            };
        }
    }
}
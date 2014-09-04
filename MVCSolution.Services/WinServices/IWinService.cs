using System;
using System.Collections.Generic;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services.WinServices
{
    public interface IWinService
    {
        ScheduledTask Get(Guid id);
        List<TaskDto> GetScheduledTasks(string keyword, TaskStatus? status);
        void Stop(Guid id);
        void Start(Guid id);
        void Delete(Guid id);
        void RunImmediately(Guid id);
        List<ServiceLog> GetLogs(Guid? id);
        void ClearLogs(Guid id);
    }
}

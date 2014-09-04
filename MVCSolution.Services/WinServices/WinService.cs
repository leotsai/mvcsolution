using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services.WinServices
{
    public class WinService : ServiceBase, IWinService
    {
        public ScheduledTask Get(Guid id)
        {
            using (var db = base.NewDB())
            {
                return db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<TaskDto> GetScheduledTasks(string keyword, TaskStatus? status)
        {
            using (var db = base.NewDB())
            {
                var tasks = db.ScheduledTasks.WhereByKeyword(keyword).ToList().Select(x => new TaskDto(x)).ToList();
                if (status != null)
                {
                    tasks = tasks.Where(x => x.Status == status).ToList();
                }
                return tasks;
            }
        }

        public void Stop(Guid id)
        {
            using (var db = base.NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new KnownException("Task doesn't exist");
                }
                task.IsEnabled = false;
                db.SaveChanges();
            }
        }

        public void Start(Guid id)
        {
            using (var db = base.NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new KnownException("Task doesn't exist");
                }
                task.IsEnabled = true;
                db.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = base.NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new KnownException("Task doesn't exist");
                }
                task.ServiceLogs.ToList().ForEach(x => db.ServiceLogs.Remove(x));
                db.ScheduledTasks.Remove(task);
                db.SaveChanges();
            }
        }

        public void RunImmediately(Guid id)
        {
            using (var db = base.NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new KnownException("Task doesn't exist");
                }
                task.RunImmediately = true;
                db.SaveChanges();
            }
        }

        public List<ServiceLog> GetLogs(Guid? id)
        {
            using (var db = base.NewDB())
            {
                return db.ServiceLogs.Where(x => x.TaskId == id).ToList();
            }
        }

        public void ClearLogs(Guid id)
        {
            using (var db = base.NewDB())
            {
                var logs = db.ServiceLogs.Where(x => x.TaskId == id).ToList();
                logs.ForEach(x => db.ServiceLogs.Remove(x));
                db.SaveChanges();
            }
        }
    }
}

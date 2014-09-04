using System;
using System.IO;
using System.Web.Mvc;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Services.WinServices;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class WinServiceController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new WinServiceViewModel();
            model.Build();
            return AreaView("WinService/index.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult Search(string keyword, TaskStatus? status)
        {
            var service = Ioc.GetService<IWinService>();
            var tasks = service.GetScheduledTasks(keyword, status);
            return PartialAreaView("WinService/search.cshtml", tasks);
        }

        [HttpGet]
        public PartialViewResult TaskDetails(Guid taskId)
        {
            var service = Ioc.GetService<IWinService>();
            var task = service.Get(taskId);
            return PartialAreaView("WinService/TaskDetails.cshtml", task);
        }

        [HttpPost]
        public StandardJsonResult Stop(Guid id)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IWinService>();
                service.Stop(id);
            });
            return result;
        }

        [HttpPost]
        public StandardJsonResult Start(Guid id)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IWinService>();
                service.Start(id);
            });
            return result;
        }

        [HttpGet]
        public PartialViewResult Uploader()
        {
            return PartialAreaView("WinService/uploader.cshtml");
        }

        [HttpPost]
        public StandardJsonResult Upload(Guid? id)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                if (id.HasValue)
                {
                    var service = Ioc.GetService<IWinService>();
                    var task = service.Get(id.Value);
                    if (task.GetStatus() != TaskStatus.Stopped)
                    {
                        throw new Exception("请先停止任务");
                    }
                }

                var file = Request.Files[0];
                var folder = AppContext.WinServiceDLLFolder;
                var newFilePath = Path.Combine(folder, file.FileName);
                if (System.IO.File.Exists(newFilePath) && id == null)
                {
                    throw new Exception("该DLL已经存在了。");
                }
                file.SaveAs(newFilePath);
            });
            return result;
        }

        [HttpPost]
        public StandardJsonResult Delete(Guid id)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IWinService>();
                var task = service.Get(id);
                if (task.GetStatus() != TaskStatus.Stopped)
                {
                    throw new Exception("请先停止任务");
                }
                service.Delete(id);
                var file = Path.Combine(AppContext.WinServiceDLLFolder, task.DllFileName);
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            });
            return result;
        }

        [HttpPost]
        public StandardJsonResult RunImmediately(Guid id)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IWinService>();
                var task = service.Get(id);
                var status = task.GetStatus();
                if (status != TaskStatus.Running)
                {
                    throw new Exception("任务状态必须是【" + TaskStatus.Running.GetText() + "】");
                }
                service.RunImmediately(id);
            });
            return result;
        }

        [HttpGet]
        public ActionResult Log(Guid? taskId)
        {
            var model = new WinServiceLogsViewModel(taskId);
            model.Build();
            return AreaView("WinService/log.cshtml", model);
        }

        [HttpGet]
        public ActionResult ClearLogs(Guid taskId)
        {
            var service = Ioc.GetService<IWinService>();
            service.ClearLogs(taskId);
            return RedirectToAction("log", new {id = taskId});
        }
    }
}
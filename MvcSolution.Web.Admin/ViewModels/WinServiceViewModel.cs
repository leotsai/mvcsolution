using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Services.WinServices;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class WinServiceViewModel : LayoutViewModel
    {
        public string DllFolder { get; set; }
        public List<SelectListItem> TaskStatuses { get; set; }

        public void Build()
        {
            this.DllFolder = AppContext.WinServiceDLLFolder;
            this.TaskStatuses = TaskStatusHelper.GetAll().ToSelectList(x => x.GetText(), x => x, "Any/All Task Status");
        }
    }

    public class WinServiceLogsViewModel : LayoutViewModel
    {
        private readonly Guid? _id;
        public List<ServiceLog> Logs { get; set; }

        public Guid? TaskId
        {
            get { return _id; }
        }

        public WinServiceLogsViewModel(Guid? id)
        {
            _id = id;
        }

        public void Build()
        {
            var service = Ioc.GetService<IWinService>();
            if (_id.HasValue)
            {
                this.Title = service.Get(_id.Value).Name;
            }
            else
            {
                this.Title = "Windows Service Logs";
            }
            this.Logs = service.GetLogs(_id);
        }
    }
}
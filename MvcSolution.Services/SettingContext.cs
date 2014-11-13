using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public class SettingContext
    {
        private static readonly SettingContext _instance;

        public static SettingContext Instance
        {
            get { return _instance; }
        }

        static SettingContext()
        {
            _instance = new SettingContext();
        }

        public DateTime Version { get; set; }


        public void Init()
        {
            var items = new List<Setting>();
            items.Add(new Setting("smtp.host", "smtp.gmail.com", "邮件服务器.主机xx"));
            items.Add(new Setting("smtp.port", "587", "邮件服务器.端口"));
            items.Add(new Setting("smtp.username", "MvcSolution.test@gmail.com", "邮件服务器.账号"));
            items.Add(new Setting("smtp.password", "MvcSolution", "邮件服务器.密码"));
            items.Add(new Setting("smtp.from", "MvcSolution", "邮件服务器.显示名称"));
            items.Add(new Setting("app.debugkey", "debugMvcSolution", "对于同步请求的出错页面，在页面地址加debug参数，可以显示详细的错误信息"));

            var service = Ioc.GetService<ISettingService>();
            service.Init(items);

            this.Refresh();
        }

        public void Refresh()
        {
            var service = Ioc.GetService<ISettingService>();
            var settings = service.GetAll().ToDictionary(x => x.Key, x => x.Value);
            this.SmtpHost = settings["smtp.host"];
            this.SmtpPort = settings["smtp.port"].ToInt32() ?? 25;
            this.SmtpUsername = settings["smtp.username"];
            this.SmtpPassword = settings["smtp.password"];
            this.SmtpFrom = settings["smtp.from"];
            this.DebugKey = settings["app.debugkey"];
            this.Version = DateTime.Now;
        }

        public Dictionary<string, object> GetValues()
        {
            var excludes = new[] {"Version"};
            return this.GetType().GetProperties()
                .Where(x => x.CanRead && x.CanWrite && !excludes.Contains(x.Name))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Name, x => x.GetValue(this));
        }


        public string SmtpHost { get; private set; }
        public int SmtpPort { get; private set; }
        public string SmtpUsername { get; private set; }
        public string SmtpPassword { get; private set; }
        public string SmtpFrom { get; private set; }
        public string DebugKey { get; private set; }
    }
}

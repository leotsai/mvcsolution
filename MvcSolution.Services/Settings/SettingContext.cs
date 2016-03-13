using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public class SettingContext
    {
        public static SettingContext Instance { get; }

        static SettingContext()
        {
            Instance = new SettingContext();
        }

        public DateTime Version { get; set; }


        public void Init()
        {
            var items = new List<Setting>();
            items.Add(new Setting("app.contactPhone", "400-603-2015", "Contact Phone Number"));
            items.Add(new Setting("app.enableSMS", "False", "Enable SMS。True|False"));
            items.Add(new Setting("app.logRequest", "False", "Log every request info? True|False"));
            items.Add(new Setting("app.adminOpenIds", "", "Manager Weixin OpenIds, CSV"));
            items.Add(new Setting("app.taxRate", "0.05", "Tax rate, from 0.00 - 1.00"));

            var service = Ioc.Get<ISettingService>();
            service.Init(items);

            this.Refresh();
        }

        public void Refresh()
        {
            var service = Ioc.Get<ISettingService>();
            var settings = service.GetAll().ToDictionary(x => x.Key, x => x.Value);
            
            this.ContactPhone = settings["app.contactPhone"];
            this.EnableSms = settings["app.enableSMS"].Eq("true");
            this.LogRequest = settings["app.logRequest"].Eq("true");
            this.AdminOpenIds = settings["app.adminOpenIds"];
            this.TaxRate = decimal.Parse(settings["app.taxRate"]);

            this.Version = DateTime.Now;
        }

        public Dictionary<string, object> GetValues()
        {
            var excludes = new[] { "Version", };//"Categories", "Cities"
            return this.GetType().GetProperties()
                .Where(x => x.CanRead && x.CanWrite && !excludes.Contains(x.Name))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Name, x => x.GetValue(this));
        }

        public string ContactPhone { get; private set; }
        public bool EnableSms { get; private set; }
        public bool LogRequest { get; private set; }
        public string AdminOpenIds { get; private set; }
        public decimal TaxRate { get; private set; }
    }
}

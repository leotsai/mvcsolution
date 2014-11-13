using System.Collections.Generic;
using System.Linq;
using MvcSolution.Services;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class SettingIndexViewModel : LayoutViewModel
    {
        public List<SettingDto> Settings { get; set; }

        public SettingIndexViewModel BuildSettings()
        {
            var service = Ioc.GetService<ISettingService>();
            var dbSettings = service.GetAll().OrderBy(x => x.Key).ToList();
            var memSettings = SettingContext.Instance.GetValues();
            this.Settings = new List<SettingDto>();
            var index = 0;
            foreach (var pair in memSettings)
            {
                var dbSetting = dbSettings[index];
                var dto = new SettingDto()
                {
                    MemKey = pair.Key,
                    MemValue = pair.Value.ToString(),
                    DbKey = dbSetting.Key,
                    DbValue = dbSetting.Value,
                    Notes = dbSetting.Notes
                };
                this.Settings.Add(dto);
                index++;
            }
            return this;
        }
    }

    public class SettingDto
    {
        public string DbKey { get; set; }
        public string DbValue { get; set; }
        public string MemKey { get; set; }
        public string MemValue { get; set; }
        public string Notes { get; set; }
    }
}

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
            var service = Ioc.Get<ISettingService>();
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
                    MemValue = pair.Value,
                    DbKey = dbSetting.Key,
                    DbValue = dbSetting.Value,
                    Notes = dbSetting.Notes
                }.BuildIsComplexType();
                this.Settings.Add(dto);
                index++;
            }
            return this;
        }
    }

    public class SettingDto
    {
        private bool _isComplexType;

        public string DbKey { get; set; }
        public string DbValue { get; set; }
        public string MemKey { get; set; }
        public object MemValue { get; set; }
        public string Notes { get; set; }

        public bool IsComplexType
        {
            get { return _isComplexType; }
        }

        public SettingDto BuildIsComplexType()
        {
            if (MemValue == null)
            {
                _isComplexType = false;
            }
            else
            {
                var type = MemValue.GetType();
                _isComplexType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>);
            }
            return this;
        }

        public bool IsMatch()
        {
            if (this.IsComplexType)
            {
                return IsListMatch();
            }
            return (this.MemValue == null ? string.Empty : this.MemValue.ToString()) == this.DbValue;
        }

        public bool IsListMatch()
        {
            return true;
        }
    }
}

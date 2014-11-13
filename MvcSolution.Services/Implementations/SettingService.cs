using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public class SettingService : ServiceBase, ISettingService
    {
        public void Init(List<Setting> settings)
        {
            using (var db = base.NewDB())
            {
                var dbSettings = db.Settings.ToList();
                var deleted = dbSettings.Where(x => !settings.Any(s => s.Key.Eq(x.Key))).ToList();
                deleted.ForEach(x => db.Settings.Remove(x));
                foreach (var setting in settings)
                {
                    var dbSetting = dbSettings.FirstOrDefault(x => x.Key.Eq(setting.Key));
                    if (dbSetting == null)
                    {
                        setting.Id = Guid.NewGuid();
                        db.Settings.Add(setting);
                    }
                    else
                    {
                        dbSetting.Notes = setting.Notes;
                    }
                }
                db.SaveChanges();
            }
        }

        public List<Setting> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Settings.ToList();
            }
        }
    }
}

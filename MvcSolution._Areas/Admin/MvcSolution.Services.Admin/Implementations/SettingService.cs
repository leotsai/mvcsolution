using System.Linq;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services.Admin
{
    public class SettingService : ServiceBase, ISettingService
    {
        public void Update(string key, string value)
        {
            using (var db = base.NewDB())
            {
                var setting = db.Settings.FirstOrDefault(x => x.Key == key);
                if (setting == null)
                {
                    throw new KnownException("KEY不存在");
                }
                setting.Value = value;
                db.SaveChanges();
            }
        }
    }
}

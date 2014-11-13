using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Libs.Statistics.Entities;
using MvcSolution.Libs.Statistics.Enums;

namespace MvcSolution.Libs.Statistics.Implementations
{
    public class AnalysisService : ServiceBase, IAnalysisService
    {
        public void AddLog(int type, Platform? platform)
        {
            try
            {
                using (var db = base.NewDB())
                {
                    var dbLog = db.RequestLogs.Get(type, platform);
                    if (dbLog == null)
                    {
                        dbLog = new RequestLog(type, platform);
                        db.RequestLogs.Add(dbLog);
                    }
                    dbLog.AddValue();
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void AddLogs(int type, List<PlatformLog> logs)
        {
            using (var db = base.NewDB())
            {
                var groups = logs.GroupBy(x => x.Time.ToGroupHour()).ToList();
                var hours = groups.Select(x => x.Key).ToArray();
                var existings = db.RequestLogs.Where(x => x.Type == type && hours.Contains(x.GroupHour)).ToList();
                foreach (var group in groups)
                {
                    foreach (var platform in group.GroupBy(x => x.Platform))
                    {
                        var existing = existings.FirstOrDefault(x=>x.GroupHour == group.Key&&x.Platform == platform.Key);
                        if (existing == null)
                        {
                            var time = platform.Max(x => x.Time);
                            var log = new RequestLog(type, platform.Key, time, platform.Count());
                            db.RequestLogs.Add(log);
                        }
                        else
                        {
                            existing.Value += platform.Count();
                            existing.Date = DateTime.Now;
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public DateTime? GetLatestLogTime(int type)
        {
            using (var db = base.NewDB())
            {
                return db.RequestLogs.Where(x => x.Type == type).Max(x => (DateTime?) x.Date);
            }
        }
    }
}

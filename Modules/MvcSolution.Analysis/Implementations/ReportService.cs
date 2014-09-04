using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis.Implementations
{
    public class ReportService : ServiceBase, IReportService
    {
        public List<RequestGroup> GetGroups(int[] types, Frequency frequency, Platform? platform, DateTime? from, DateTime? to)
        {
            using (var db = base.NewDB())
            {
                var items = db.RequestLogs
                    .WhereByPlatform(platform)
                    .WhereByFromDate(from)
                    .WhereByToDate(to)
                    .WhereByTypes(types)
                    .ToGroupedRequestDtos(frequency)
                    .OrderBy(x => x.Group)
                    .ToList();
                var min = (from == null ? db.RequestLogs.Min(x => x.Date) : from.Value);
                var max = (to == null ? DateTime.Today : to.Value);
                if (frequency == Frequency.Hour)
                {
                    max = max.AddHours(to == DateTime.Today ? DateTime.Now.Hour : 23);
                }
                if (frequency.CalculateItems(min, max) > 62)
                {
                    throw new Exception("数据太多，请调整频率或者时间段");
                }

                var groups = new List<RequestGroup>();
                foreach (var type in types)
                {
                    var typeItems = items.Where(x => x.Type == type).ToList();
                    var group = new RequestGroup(type);
                    for (var value = min; value <= max; value = frequency.GetNextTime(value))
                    {
                        var valueGroup = value.ToGroupValue(frequency);
                        var item = typeItems.FirstOrDefault(x => x.Group == valueGroup);
                        group.Items.Add(new RequestItem(value.ToString(), item == null ? 0 : item.Value));
                    }
                    groups.Add(group);
                }
                return groups;
            }
        }

        public List<RequestGroup> GetPlatformsGroups(int typex, Frequency frequency, DateTime? @from, DateTime? to)
        {
            using (var db = base.NewDB())
            {
                var items = db.RequestLogs
                    .WhereByFromDate(from)
                    .WhereByToDate(to)
                    .WhereByType(typex)
                    .ToGroupedPlatformRequestDtos(frequency)
                    .OrderBy(x => x.Group)
                    .ToList();
                var min = (from == null ? db.RequestLogs.Min(x => x.Date) : from.Value);
                var max = (to == null ? DateTime.Today : to.Value);
                if (frequency == Frequency.Hour)
                {
                    max = max.AddHours(DateTime.Now.Hour);
                }
                if (frequency.CalculateItems(min, max) > 62)
                {
                    throw new Exception("数据太多，请调整频率或者时间段");
                }
                var platforms = new List<Platform?>();
                platforms.Add(null);
                platforms.Add(Platform.Android);
                platforms.Add(Platform.Ios);
                var groups = new List<RequestGroup>();
                foreach (var platform in platforms)
                {
                    var typeItems = items.Where(x => x.Platform == platform).ToList();
                    var group = new RequestGroup(platform);
                    for (var value = min; value <= max; value = frequency.GetNextTime(value))
                    {
                        var valueGroup = value.ToGroupValue(frequency);
                        var item = typeItems.FirstOrDefault(x => x.Group == valueGroup);
                        group.Items.Add(new RequestItem(value.ToString(), item == null ? 0 : item.Value));
                    }
                    groups.Add(group);
                }
                return groups;
            }
        }
    }
}

using System.Data.Entity;
using MvcSolution.Libs.Statistics.Entities;

namespace MvcSolution.Libs.Statistics.Context
{
    public class StatisticsDataContext : DbContext
    {
        public DbSet<RequestLog> RequestLogs { get; set; }

        public StatisticsDataContext()
        {
            Database.SetInitializer<StatisticsDataContext>(null);
        }
    }
}

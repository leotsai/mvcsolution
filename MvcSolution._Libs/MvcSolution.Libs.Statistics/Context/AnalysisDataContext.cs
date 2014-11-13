using System.Data.Entity;
using MvcSolution.Libs.Statistics.Entities;

namespace MvcSolution.Libs.Statistics.Context
{
    public class AnalysisDataContext : DbContext
    {
        public DbSet<RequestLog> RequestLogs { get; set; }

        public AnalysisDataContext()
        {
            Database.SetInitializer<AnalysisDataContext>(null);
        }
    }
}

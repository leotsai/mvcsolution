using System.Data.Entity;
using MvcSolution.Analysis.Entities;

namespace MvcSolution.Analysis.Context
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

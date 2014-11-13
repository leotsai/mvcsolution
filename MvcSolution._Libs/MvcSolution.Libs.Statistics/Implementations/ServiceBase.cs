using MvcSolution.Libs.Statistics.Context;

namespace MvcSolution.Libs.Statistics.Implementations
{
    public class ServiceBase
    {
        protected StatisticsDataContext NewDB()
        {
            return new StatisticsDataContext();
        }
    }
}

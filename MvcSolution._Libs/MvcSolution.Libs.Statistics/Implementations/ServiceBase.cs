using MvcSolution.Libs.Statistics.Context;

namespace MvcSolution.Libs.Statistics.Implementations
{
    public class ServiceBase
    {
        protected AnalysisDataContext NewDB()
        {
            return new AnalysisDataContext();
        }
    }
}

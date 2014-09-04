using MvcSolution.Analysis.Context;

namespace MvcSolution.Analysis.Implementations
{
    public class ServiceBase
    {
        protected AnalysisDataContext NewDB()
        {
            return new AnalysisDataContext();
        }
    }
}

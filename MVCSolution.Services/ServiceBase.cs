using MvcSolution.Data.Context;

namespace MvcSolution.Services
{
    public abstract class ServiceBase
    {
        protected MvcSolutionDataContext NewDB()
        {
            return new MvcSolutionDataContext();
        }
    }
}

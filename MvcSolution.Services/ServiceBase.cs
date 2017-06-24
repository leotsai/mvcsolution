using System;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public interface IServiceBase
    {

    }

    public abstract class ServiceBase
    {
        protected MvcSolutionDbContext NewDB()
        {
            return new MvcSolutionDbContext();
        }

        protected void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogHelper.TryLog("ServiceBase.Try", ex);
            }
        }
    }
}

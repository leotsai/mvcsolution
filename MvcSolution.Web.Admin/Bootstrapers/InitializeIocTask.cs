using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcSolution.Analysis;
using MvcSolution.Analysis.Implementations;
using MvcSolution.Infrastructure;
using ServiceBase = MvcSolution.Services.ServiceBase;

namespace MvcSolution.Web.Admin.Bootstrapers
{
    public class InitializeIocTask
    {
        private readonly IUnityContainer _unityContainer;
        public InitializeIocTask(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #region Implementation of IBootstraperTask

        public void Execute()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(_unityContainer));
            _unityContainer.RegisterInheritedTypes(typeof(ServiceBase).Assembly, typeof(ServiceBase));
            _unityContainer.RegisterType<IReportService, ReportService>();
        }

        #endregion
    }
}
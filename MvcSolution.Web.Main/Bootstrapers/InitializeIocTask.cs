using System.Web.Mvc;
using MVCSolution.Services;
using MvcSolution.Infrastructure;
using Microsoft.Practices.Unity;

namespace MvcSolution.Web.Main.Bootstrapers
{
    public class InitializeIocTask : IBootstraperTask
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
            _unityContainer.RegisterInheritedTypes(typeof(ServiceBase<>));
        }

        #endregion
    }
}
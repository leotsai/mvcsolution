using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace MvcSolution.Infrastructure
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }
        #region Implementation of IDependencyResolver

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }

        #endregion
    }
}

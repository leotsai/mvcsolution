using System;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace MvcSolution
{
    public class Ioc
    {
        private static readonly UnityContainer _container;

        static Ioc()
        {
            _container = new UnityContainer();
        }

        public static void Register<TInterface, TImpmentation>() where TImpmentation : TInterface
        {
            _container.RegisterType<TInterface, TImpmentation>();
        }

        public static void RegisterInheritedTypes(Assembly assembly, Type baseType)
        {
            _container.RegisterInheritedTypes(assembly, baseType);
        }

        public static T GetService<T>()
        {
            return _container.Resolve<T>();
        }
    }
}

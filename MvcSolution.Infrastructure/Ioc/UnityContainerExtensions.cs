using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace MvcSolution
{
    public static class UnityContainerExtensions
    {
        public static void RegisterInheritedTypes(this IUnityContainer container, Assembly assembly, Type baseType)
        {
            var allTypes = assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();
            foreach (var type in allTypes)
            {
                if (type.BaseType != null && type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = type.GetInterfaces().FirstOrDefault(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
                    if (typeInterface == null)
                    {
                        continue;
                    }
                    container.RegisterType(typeInterface, type);
                }
            }
        }
    }
}

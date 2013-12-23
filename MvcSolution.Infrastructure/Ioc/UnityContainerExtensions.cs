using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace MvcSolution
{
    public static class UnityContainerExtensions
    {
        public static void RegisterInheritedTypes<T>(this IUnityContainer container)
        {
            var baseType = typeof (T);
            container.RegisterInheritedTypes(baseType);
        }

        public static void RegisterInheritedTypes(this IUnityContainer container, Type baseType)
        {
            var allTypes = baseType.Assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();
            foreach (var type in allTypes)
            {
                if (type.BaseType != null && type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = type.GetInterfaces()
                        .FirstOrDefault(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
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

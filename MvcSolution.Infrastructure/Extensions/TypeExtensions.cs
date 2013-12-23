using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution
{
    public static class TypeExtensions
    {
        public static bool GenericEq(this Type type, Type toCompare)
        {
            return type.Namespace == toCompare.Namespace && type.Name == toCompare.Name;
        }
    }
}

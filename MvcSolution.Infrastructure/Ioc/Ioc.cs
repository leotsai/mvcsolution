using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcSolution
{
    public class Ioc
    {
        public static T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}

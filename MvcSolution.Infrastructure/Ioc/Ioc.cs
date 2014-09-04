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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Data.Enums;

namespace MvcSolution.Data.Enums
{
    public enum Platform
    {
        Android = 1,
        Ios = 2
    }
}

namespace MvcSolution
{
    public static class PlatformHelper
    {
        public static List<Platform> GetAll()
        {
            return new List<Platform>()
            {
                Platform.Android,
                Platform.Ios
            };
        }

        public static string GetText(this Platform status)
        {
            switch (status)
            {
                case Platform.Ios:
                    return "IOS";
                case Platform.Android:
                    return "Android";
                default:
                    return "All";
            }
        }
    }
}
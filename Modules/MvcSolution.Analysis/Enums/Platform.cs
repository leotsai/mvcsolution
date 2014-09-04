using System.Collections.Generic;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis.Enums
{
    public enum Platform
    {
        Android = 1,
        Ios = 2
    }
}

namespace MvcSolution.Analysis
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
                    return "IOS平台";
                case Platform.Android:
                    return "安卓平台";
                default:
                    return "所有平台";
            }
        }
    }
}
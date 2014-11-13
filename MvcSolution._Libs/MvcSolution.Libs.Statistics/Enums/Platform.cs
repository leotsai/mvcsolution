using System.Collections.Generic;
using MvcSolution.Libs.Statistics.Enums;

namespace MvcSolution.Libs.Statistics.Enums
{
    public enum Platform
    {
        Android = 1,
        Ios = 2
    }
}

namespace MvcSolution.Libs.Statistics
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
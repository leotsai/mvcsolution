using System;
using System.Configuration;

namespace MvcSolution
{
    public class AppContext
    {
        /// <summary>
        /// D:\wwws\www.MvcSolution.com
        /// </summary>
        public static string RootFolder { get; }

        

        static AppContext()
        {
            RootFolder = ConfigurationManager.AppSettings["RootFolder"];
        }

        public const int LoginExpireDays = 365;
        public const string Md5Key = "MvcSolution";

        /// <summary>
        /// returns /_storage/image originals/{userid}/{year}-{month}/
        /// </summary>
        public static string GetImageOriginalFolder(Guid? userId)
        {
            return $"/_storage/image originals/{(userId == null ? "_system" : userId.ToString())}/{DateTime.Now.Year}-{DateTime.Now.Month}/";
        }

        /// <summary>
        /// returns /_storage/image sized/{year}-{month}/{imageId}/
        /// </summary>
        public static string GetSizedImageFolder(Guid imageId)
        {
            return $"/_storage/image sized/{DateTime.Now.Year}-{DateTime.Now.Month}/{imageId}/";
        }
    }
}

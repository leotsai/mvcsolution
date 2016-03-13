using System;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public static class ImageHelper
    {
        /// <summary>
        /// returns "/img/{size}/default-{name}.{format}", "img/{size}/t{imageType}t{yearMonth}-{id}.{format}"
        /// </summary>
        /// <param name="key">
        /// t20t201410-6C294E0437664E3FA99C0CBAA4E8CCD1.png
        /// default-websitelogo.png
        /// </param>
        /// <param name="size"></param>
        /// <returns>
        /// e.g. /img/full/t20t201410-6C294E0437664E3FA99C0CBAA4E8CCD1.png
        /// e.g. /img/s80x80/t20t201410-6C294E0437664E3FA99C0CBAA4E8CCD1.png
        /// e.g. /img/s80x80/default-websitelogo.png
        /// </returns>
        public static string BuildSrc(string key, ImageSize size)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "/_storage/css/404.png";
            }
            if (key.IndexOf("/", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return string.Format("/img/{0}/{1}", size, key);
            }
            return key;
        }
    }
}

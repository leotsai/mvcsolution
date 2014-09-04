using System;
using System.Configuration;
using System.Linq;

namespace MvcSolution
{
    public class AppContext
    {
        private static readonly bool _isTestServer = false;
        private static readonly string _host;
        private static readonly string _winserviceDllFolder;

        public static bool IsTestServer
        {
            get { return _isTestServer; }
        }

        public static string WinServiceDLLFolder
        {
            get { return _winserviceDllFolder; }
        }

        /// <summary>
        /// http://192.168.1.111
        /// </summary>
        public static string Host
        {
            get { return _host; }
        }

        static AppContext()
        {
            var serverRoles = ConfigurationManager.AppSettings["ServerRoles"];
            if (!string.IsNullOrEmpty(serverRoles))
            {
                var roles = serverRoles.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (roles.Contains("TestServer"))
                {
                    _isTestServer = true;
                }
            }
            _host = ConfigurationManager.AppSettings["Host"];
            if (string.IsNullOrEmpty(_host))
            {
                _host = "http://192.168.1.111:10020";
            }
            _winserviceDllFolder = ConfigurationManager.AppSettings["WinServiceDLLFolder"];
        }
    }
}

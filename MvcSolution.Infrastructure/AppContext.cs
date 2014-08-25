using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution
{
    public class AppContext
    {
        private static readonly bool _isTestServer = false;
        private static readonly string _host ;

        public static bool IsTestServer 
        {
            get { return _isTestServer; }
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

        }
    }
}

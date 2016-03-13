using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution
{
    public static class IpHelper
    {
        public static string GetAddress(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                return string.Empty;
            }
            return "ip address";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution
{
    public static class IpHelper
    {
        public static string GetAddress(string ip)
        {
            var url = "http://ip.taobao.com/service/getIpInfo.php?ip=" + ip;
            try
            {
                using (var client = new WebClient())
                {
                    var html = client.DownloadString(url);
                    return Serializer.FromJson<IpEntity>(html).GetAddress();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    internal class IpEntity
    {
        public string Code { get; set; }
        public IpEntityValue Data { get; set; }

        public string GetAddress()
        {
            if (this.Data == null)
            {
                return string.Empty;
            }
            return this.Data.Country + this.Data.City;
        }
    }

    internal class IpEntityValue
    {
        public string Country { get; set; }
        public string City { get; set; }
    }
}

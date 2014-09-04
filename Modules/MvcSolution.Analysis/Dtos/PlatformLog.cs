using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    public class PlatformLog
    {
        public Platform? Platform { get; set; }
        public DateTime Time { get; set; }

        public PlatformLog(int? platform, DateTime time)
        {
            this.Platform = (Platform?) platform;
            this.Time = time;
        }
    }
}

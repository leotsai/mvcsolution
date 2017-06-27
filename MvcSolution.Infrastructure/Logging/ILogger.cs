using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution
{
    public interface ILogger
    {
        void Entry(string group, string message);
        void Start();
        void Stop();
    }
}

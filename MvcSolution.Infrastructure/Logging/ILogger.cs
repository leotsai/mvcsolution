using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSolution.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(object message);
    }
}

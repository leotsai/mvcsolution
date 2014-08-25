using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution.Infrastructure
{
    public class KnownException : Exception
    {
        public KnownException(string message) : base(message)
        {

        }
    }
}

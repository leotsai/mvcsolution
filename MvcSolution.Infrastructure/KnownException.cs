using System;

namespace MvcSolution.Infrastructure
{
    public class KnownException : Exception
    {
        public KnownException(string message) : base(message)
        {

        }
    }
}

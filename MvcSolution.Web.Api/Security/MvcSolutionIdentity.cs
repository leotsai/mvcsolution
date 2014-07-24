using System;

namespace MvcSolution.Web.Api.Security
{
    public class MvcSolutionIdentity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }

        public MvcSolutionIdentity()
        {
            this.CreatedTime = DateTime.Now;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
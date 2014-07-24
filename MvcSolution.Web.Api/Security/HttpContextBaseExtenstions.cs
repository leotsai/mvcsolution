using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSolution.Web.Api.Security;

namespace MvcSolution
{
    public static class HttpContextBaseExtenstionsApiSecutity
    {
        public static void AddIdentity(this HttpContextBase context, MvcSolutionIdentity identity)
        {
            context.Items.Add("Identity", identity);
        }

        public static MvcSolutionIdentity GetIdentity(this HttpContextBase context)
        {
            return context.Items["Identity"] as MvcSolutionIdentity;
        }
    }
}
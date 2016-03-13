using System;
using MvcSolution.Web.Controllers;

namespace MvcSolution.Web.Public.Controllers
{
    public class PublicControllerBase : MvcSolutionControllerBase
    {
        protected override string AreaName => "public";
    }
}

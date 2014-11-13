using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Web.Controllers;

namespace MvcSolution.Web.Public.Controllers
{
    public abstract class PublicControllerBase : MvcSolutionControllerBase
    {
        protected override string AreaName
        {
            get { return PublicAreaRegistration.Name; }
        }
    }
}
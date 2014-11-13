using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Web.Controllers;

namespace MvcSolution.Web.Admin.Controllers
{
    //[Authorize(Roles = Role.Names.SuperAdmin)]
    public abstract class AdminControllerBase : MvcSolutionControllerBase
    {
        protected override string AreaName
        {
            get { return AdminAreaRegistration.Name; }
        }
    }
}
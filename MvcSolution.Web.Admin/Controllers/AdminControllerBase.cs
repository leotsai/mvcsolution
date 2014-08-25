using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Web.Controllers;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Admin.Controllers
{
    [Authorize(Roles = Role.Names.SuperAdmin)]
    public class AdminControllerBase : AreaControllerBase
    {
        protected override string AreaName
        {
            get { return AdminAreaRegistration.Name; }
        }
    }
}

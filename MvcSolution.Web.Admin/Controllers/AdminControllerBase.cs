using System.Web.Mvc;
using MvcSolution.Web.Controllers;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Admin.Controllers
{
    //[Authorize(Roles = RoleNames.Admin)]
    public class AdminControllerBase : AreaControllerBase
    {
        protected override string AreaName
        {
            get { return AdminAreaRegistration.Name; }
        }
    }
}

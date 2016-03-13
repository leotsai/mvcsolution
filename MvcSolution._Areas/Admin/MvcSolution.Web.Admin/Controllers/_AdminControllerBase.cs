using MvcSolution.Data;
using MvcSolution.Web.Controllers;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Admin.Controllers
{
    [MvcAuthorize(Role.Names.SuperAdmin)]
    public class AdminControllerBase : MvcSolutionControllerBase
    {
        protected override string AreaName => "admin";
    }
}

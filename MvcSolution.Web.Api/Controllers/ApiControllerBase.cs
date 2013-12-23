using MvcSolution.Web.Controllers;

namespace MvcSolution.Web.Api.Controllers
{
    public class ApiControllerBase : AreaControllerBase
    {
        protected override string AreaName
        {
            get { return ApiAreaRegistration.Name; }
        }
    }
}

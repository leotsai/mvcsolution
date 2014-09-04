using Microsoft.Practices.Unity;
using MvcSolution.Web;
using MvcSolution.Web.Admin.Bootstrapers;

namespace MvcSolution.Web.Admin
{
    public class MvcSolutionAdminApplication : MvcApplication
    {
        protected override void OnApplicationStart()
        {
            new InitializeIocTask(new UnityContainer()).Execute();
        }
    }
}
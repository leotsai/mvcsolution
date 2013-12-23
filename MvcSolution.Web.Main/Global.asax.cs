using MvcSolution.Web.Main.Bootstrapers;

namespace MvcSolution.Web.Main
{
    public class MvcSolutionApplication : MvcApplication
    {
        protected override void OnApplicationStart()
        {
            new Bootstraper().Run();
        }
    }
}
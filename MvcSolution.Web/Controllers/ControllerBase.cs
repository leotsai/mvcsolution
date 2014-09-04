using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Controllers
{
    public class MvcSolutionControllerBase : Controller
    {
        protected ActionResult Error(string message)
        {
            var viewModel = new ErrorCommonViewModel();
            viewModel.Message = message;
            return View("/views/error/common.cshtml", viewModel);
        }

    }
}

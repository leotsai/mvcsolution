using System.Web.Mvc;

namespace MvcSolution.Web.Controllers
{
    public abstract class AreaControllerBase : MvcSolutionControllerBase
    {
        protected abstract string AreaName { get; }

        /// <summary>
        /// path e.g. home/index
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ViewResult AreaView(string path)
        {
            return AreaView(path, null);
        }

        /// <summary>
        /// path e.g. home/index
        /// </summary>
        /// <param name="path"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ViewResult AreaView(string path, object model)
        {
            var fullPath = string.Format("~/Areas/{0}/Views/{1}.cshtml", AreaName, path);
            return model == null ? View(fullPath) : View(fullPath, model);
        }
    }
}

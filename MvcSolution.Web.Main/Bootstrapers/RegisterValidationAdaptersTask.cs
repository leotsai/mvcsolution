using MvcSolution.Infrastructure.Mvc.Validation;

namespace MvcSolution.Web.Main.Bootstrapers
{
    public class RegisterValidationAdaptersTask : IBootstraperTask
    {
        #region Implementation of IBootstraperTask

        public void Execute()
        {
            DataAnnotationsModelValidatorProviderHelper.RegisterAdapters();
        }

        #endregion
    }
}
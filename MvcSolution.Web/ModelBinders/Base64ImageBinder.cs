using System.Web.Mvc;

namespace MvcSolution.Web
{
    public class Base64ImageBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue("base64image");
            if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
            {
                return null;
            }
            return new Base64Image(value.AttemptedValue).Build();
        }
    }
}

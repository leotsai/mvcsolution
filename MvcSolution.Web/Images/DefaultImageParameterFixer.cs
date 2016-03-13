using System;

namespace MvcSolution.Web.Images
{
    public class DefaultImageParameterFixer : IImageParameterFixer
    {
        public ImageParameter Fix(ImageParameter parameter)
        {
            parameter.Guid = Guid.Parse(parameter.Id);
            parameter.Year = parameter.YearMonth / 100;
            parameter.Month = parameter.YearMonth % 100;
            if (parameter.Year < 2014 || parameter.Year > DateTime.Now.Year + 1 || parameter.Month < 1 || parameter.Month > 12)
            {
                throw new Exception("错误的年月格式");
            }
            parameter.ImageFormat = ("." + parameter.Format).GetImageFormat();
            return parameter;
        }
    }
}

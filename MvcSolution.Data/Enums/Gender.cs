using MvcSolution.Data;

namespace MvcSolution.Data
{
    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }
}

namespace MvcSolution
{
    public static class GenderHelper
    {
        public static string GetText(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "男";
                case Gender.Female:
                    return "女";
            }
            return "未知";
        }
    }
}
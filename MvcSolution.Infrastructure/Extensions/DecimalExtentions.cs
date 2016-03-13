namespace MvcSolution
{
    public static class DecimalExtentions
    {
        public static string ToText(this decimal value)
        {
            return value.ToString("g0");
        }
    }
}

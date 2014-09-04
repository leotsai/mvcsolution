namespace MvcSolution
{
    public static class NullableExtensions
    {
        public static T To<T>(this T? value) where T : struct
        {
            return To(value, default(T));
        }

        public static T To<T>(this T? value, T defaultValue) where T : struct
        {
            return value.HasValue ? value.Value : defaultValue;
        }
    }
}

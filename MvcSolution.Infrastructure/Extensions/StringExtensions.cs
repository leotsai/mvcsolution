using System;

namespace MvcSolution
{
    public static class StringExtensions
    {
        public static bool Eq(this string input, string toCompare, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return input.Equals(toCompare, comparison);
        }

        public static int? ToInt32(this string str)
        {
            int value;
            if (int.TryParse(str, out value))
            {
                return value;
            }
            return null;
        }

        public static string ToWords(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            var chars = input.ToCharArray();
            var result = string.Empty;
            foreach (var c in chars)
            {
                var str = c.ToString();
                if (str == str.ToUpper())
                {
                    result += " ";
                }
                result += str;
            }
            return result;
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace MvcSolution
{
    public static class StringExtensions
    {
        public static bool Eq(this string input, string toCompare, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (input == null)
            {
                return toCompare == null;
            }
            return input.Equals(toCompare, comparison);
        }
        
        public static Guid? ToGuid(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            Guid id;
            if (Guid.TryParse(str, out id))
            {
                return id;
            }
            return null;
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
        
        public static bool IsEmail(this string value)
        {
            var reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return string.IsNullOrEmpty(value) == false && reg.IsMatch(value);
        }
    }
}

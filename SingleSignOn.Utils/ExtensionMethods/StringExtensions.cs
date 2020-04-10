using System;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static int ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }
    }
}

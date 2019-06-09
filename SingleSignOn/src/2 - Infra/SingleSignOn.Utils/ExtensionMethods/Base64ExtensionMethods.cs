using System;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class Base64ExtensionMethods
    {
        public static string ToBase64(this string str)
        {
            var byteArray = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(byteArray);
        }

        public static string FromBase64(this string base64EncodedString)
        {
            byte[] byteArray = Convert.FromBase64String(base64EncodedString);
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }


        public static byte[] FromBase64ToByteArray(this string base64EncodedString)
        {
            return Convert.FromBase64String(base64EncodedString);
        }
    }
}

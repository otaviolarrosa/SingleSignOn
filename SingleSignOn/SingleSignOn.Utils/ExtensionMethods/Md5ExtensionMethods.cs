using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class Md5ExtensionMethods
    {
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(i.ToString("x2"));

            return sBuilder.ToString();
        }

        public static bool IsValidMd5(this string str)
        {
            return str.IsNullOrEmpty() ? false : Regex.IsMatch(str, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }


        public static string Md5Encypt(this string str)
        {
            string hash;
            using (MD5 md5 = MD5.Create())
                hash = GetMd5Hash(md5, str);
            return hash;
        }
    }
}

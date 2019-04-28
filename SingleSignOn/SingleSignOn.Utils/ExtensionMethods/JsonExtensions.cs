using Newtonsoft.Json;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object ToJsonObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

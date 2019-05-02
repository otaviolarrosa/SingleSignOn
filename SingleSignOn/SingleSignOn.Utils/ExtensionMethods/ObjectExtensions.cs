namespace SingleSignOn.Utils
{
    public static class ObjectExtensions
    {
        public static int ToInt(this object obj)
        {
            return (int)obj;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}

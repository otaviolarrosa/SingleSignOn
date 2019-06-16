namespace SingleSignOn.Utils
{
    public static class AppSettings
    {
        public static string MongoDbConnectionString;
        public static string MongoDbDatabaseName;
        public static string RedisHost;
        public static string RedisInstanceName;
        public static string SecretKey;
        public static int ExpireTokenInMinutes;
    }
}

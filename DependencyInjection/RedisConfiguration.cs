namespace QlhsServer.DependencyInjection
{
    public static class RedisConfiguration
    {
        public static void AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = "tuyenpham.site:6379";

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
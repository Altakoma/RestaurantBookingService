using CatalogService.Domain.Exceptions;

namespace CatalogService.Presentation.Configurations
{
    public static class RedisConfiguration
    {
        public const string RedisServerString = "Redis";

        public static IServiceCollection ConfigureRedis(
            this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(RedisServerString)!;

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = connectionString;
            });

            return services;
        }
    }
}

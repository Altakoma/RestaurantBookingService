using IdentityService.DataAccess.Exceptions;

namespace IdentityService.API.Configurations
{
    public static class RedisConfiguration
    {
        public const string RedisServerString = "Redis";

        public static IServiceCollection ConfigureRedis(
            this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = Environment.GetEnvironmentVariable(RedisServerString)
                ?? configuration.GetConnectionString(RedisServerString);

            if (connectionString == null)
            {
                throw new NotFoundException(nameof(connectionString), typeof(string));
            }

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = connectionString;
            });

            return services;
        }
    }
}

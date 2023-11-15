using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string MongoDbConnectionString = "MongoDbConnection";
        public const string SqlDbConnectionString = "SqlConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<OrderServiceSqlDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(SqlDbConnectionString));
            });

            services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection(MongoDbConnectionString));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            return services;
        }
    }
}

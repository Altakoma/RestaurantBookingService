using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<OrderServiceSqlDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("SqlConnection"));
            });

            services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbConnection"));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            return services;
        }
    }
}

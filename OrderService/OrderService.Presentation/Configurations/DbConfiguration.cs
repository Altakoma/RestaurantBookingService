using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string MongoDbConnectionString = "MongoDbConnection";
        public const string MongoDbName = "MongoDbName";
        public const string SqlDbDevelopmentConnectionString = "DefaultSqlConnection";
        public const string EnvironmentSqlDbDevelopmentConnectionString = "DefaultSqlConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<OrderServiceSqlDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString(SqlDbDevelopmentConnectionString));
                }
                else
                {
                    options.UseSqlServer(
                        Environment.GetEnvironmentVariable(EnvironmentSqlDbDevelopmentConnectionString));
                }
            });

            if (builder.Environment.IsDevelopment())
            {
                services.Configure<MongoDbSettings>(
                    builder.Configuration.GetSection(MongoDbConnectionString));

                services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            }
            else
            {
                string? mongoDbConnectionString =
                    Environment.GetEnvironmentVariable(MongoDbConnectionString);

                string? mongoDbName = Environment.GetEnvironmentVariable(MongoDbName);

                if (mongoDbConnectionString is null || mongoDbName is null)
                {
                    throw new BadConfigurationProvidedException(
                        string.Format(ExceptionMessages.BadConfigurationProvidedMessage,
                                      MongoDbConnectionString));
                }

                var configuration = new MongoDbSettings
                {
                    ConnectionString = mongoDbConnectionString,
                    DatabaseName = mongoDbName,
                };

                services.AddSingleton<IMongoDbSettings>(configuration);
            }

            return services;
        }
    }
}

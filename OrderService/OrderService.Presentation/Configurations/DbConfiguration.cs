using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using System.Text.Json;

namespace OrderService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string MongoDbConnectionSection = "MongoDbConnection";
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
                    builder.Configuration.GetSection(MongoDbConnectionSection));

                services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            }
            else
            {
                string? mongoDbConnectionSection = 
                    Environment.GetEnvironmentVariable(MongoDbConnectionSection);

                if (mongoDbConnectionSection is null)
                {
                    throw new BadConfigurationProvidedException(
                        string.Format(ExceptionMessages.BadConfigurationProvidedMessage,
                                      MongoDbConnectionSection));
                }

                MongoDbSettings? configuration =
                    JsonSerializer.Deserialize<MongoDbSettings>(mongoDbConnectionSection);

                if (configuration is null)
                {
                    throw new BadConfigurationProvidedException(
                        string.Format(ExceptionMessages.BadConfigurationProvidedMessage,
                                      MongoDbConnectionSection));
                }

                services.AddSingleton<IMongoDbSettings>(configuration);
            }

            return services;
        }
    }
}

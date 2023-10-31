using Hangfire;
using Microsoft.Data.SqlClient;

namespace OrderService.Presentation.Configurations
{
    public static class HangfireConfiguration
    {
        public const string DefaultDbConnectionString = "DefaultSqlConnection";

        public static IServiceCollection AddHangfire(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            string? hangfireConnectionString;

            if (builder.Environment.IsDevelopment())
            {
                hangfireConnectionString = builder.Configuration
                    .GetConnectionString(DefaultDbConnectionString);
            }
            else
            {
                hangfireConnectionString = Environment
                    .GetEnvironmentVariable(DefaultDbConnectionString);
            }

            services.AddHangfire(globalConfiguration =>
                    globalConfiguration.UseSqlServerStorage(
                    () => new SqlConnection(hangfireConnectionString)));

            services.AddHangfireServer();

            return services;
        }
    }
}

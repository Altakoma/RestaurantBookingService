using Hangfire;
using Microsoft.Data.SqlClient;

namespace OrderService.Presentation.Configurations
{
    public static class HangfireConfiguration
    {
        public const string DefaultDbConnectionString = "DefaultSqlConnection";

        public static IServiceCollection AddHangfire(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? hangfireConnectionString = configuration.GetConnectionString(DefaultDbConnectionString);

            services.AddHangfire(globalConfiguration =>
                    globalConfiguration.UseSqlServerStorage(() => new SqlConnection(hangfireConnectionString)));

            services.AddHangfireServer();

            return services;
        }
    }
}

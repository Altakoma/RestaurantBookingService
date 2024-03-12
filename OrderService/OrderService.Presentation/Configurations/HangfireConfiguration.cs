using Hangfire;
using Microsoft.Data.SqlClient;

namespace OrderService.Presentation.Configurations
{
    public static class HangfireConfiguration
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            string hangfireConnectionString = builder.Configuration
                .GetConnectionString("SqlConnection")!;

            services.AddHangfire(globalConfiguration =>
                    globalConfiguration.UseSqlServerStorage(
                    () => new SqlConnection(hangfireConnectionString)));

            services.AddHangfireServer();

            return services;
        }
    }
}

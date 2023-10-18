using BookingService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string DbDevelopmentConnectionString = "ConnectionStrings:DefaultConnection";
        public const string DbEnvironmentConnectionString = "DefaultConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<BookingServiceDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseNpgsql(builder.Configuration[DbDevelopmentConnectionString]);
                }
                else
                {
                    options.UseNpgsql(builder.Configuration[DbEnvironmentConnectionString]);
                }
            });

            return services;
        }
    }
}

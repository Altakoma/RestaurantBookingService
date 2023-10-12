using IdentityService.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Configurations
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                }
                else
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
                }
            });

            return services;
        }
    }
}

﻿using BookingService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string DbConnectionString = "SqlConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<BookingServiceDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration
                                         .GetConnectionString(DbConnectionString));
            });

            return services;
        }
    }
}

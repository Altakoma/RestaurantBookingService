namespace BookingService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                }
                else
                {
                }
            });

            return services;
        }
    }
}

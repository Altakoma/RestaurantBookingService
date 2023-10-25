using BookingService.Infrastructure.Data;
using Microsoft.AspNetCore.Diagnostics;
using OrderService.Presentation.Configurations;

namespace OrderService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder);

            var app = builder.Build();

            var seed = app.Services.GetRequiredService<Seed>();

            seed.SeedData();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
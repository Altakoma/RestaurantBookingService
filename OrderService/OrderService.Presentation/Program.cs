using Hangfire;
using OrderService.Infrastructure.Data;
using OrderService.Presentation.Configurations;
using OrderService.Presentation.Middlewares;

namespace OrderService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder);

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            var seed = app.Services.GetRequiredService<Seed>();

            seed.SeedData();

            //app.UseHangfireDashboard();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
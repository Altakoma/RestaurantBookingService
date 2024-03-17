using CatalogService.Infrastructure;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Grpc.Services.Servers;
using CatalogService.Presentation.Configurations;
using CatalogService.Presentation.Middlewares;

namespace CatalogService.Presentation
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapGrpcService<GrpcServerEmployeeService>();

            app.MapControllers();

            app.Run();
        }
    }
}

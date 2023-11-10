using IdentityService.API.Configurations;
using IdentityService.API.Middlewares;
using IdentityService.BusinessLogic.Grpc.Servers;
using IdentityService.DataAccess;

namespace IdentityService.API
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

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapGrpcService<GrpcServerEmployeeService>();

            app.Run();
        }
    }
}
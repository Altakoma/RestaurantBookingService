using BookingService.Infrastructure.Data;
using BookingService.Infrastructure.Grpc.Services.Servers;
using BookingService.Infrastructure.SignalR.Hubs;
using BookingService.Presentation.Configurations;
using BookingService.Presentation.Middlewares;

namespace BookingService.Presentation
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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapHub<BookingHub>(BookingHub.HubPattern);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            //app.MapGrpcService<GrpcServerBookingService>();

            app.MapControllers();

            app.Run();
        }
    }
}
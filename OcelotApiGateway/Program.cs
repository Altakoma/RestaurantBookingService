using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == ("localhost"))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            var app = builder.Build();

            app.UseCors("CorsPolicy");
            await app.UseOcelot();

            app.Run();
        }
    }
}
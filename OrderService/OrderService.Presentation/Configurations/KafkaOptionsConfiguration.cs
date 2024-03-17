using Microsoft.Extensions.Options;
using OrderService.Infrastructure.KafkaMessageBroker;

namespace OrderService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string bootstrapServer = configuration.GetConnectionString("BootstrapServer")!;

            string groupName = configuration["GroupName"]!;

            IOptions<KafkaOptions> options = Options.Create(new KafkaOptions
            {
                BootstrapServer = bootstrapServer,
                GroupName = groupName,
            });

            services.AddSingleton(options);

            return services;
        }
    }
}

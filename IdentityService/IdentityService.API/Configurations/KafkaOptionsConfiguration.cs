using Confluent.Kafka;
using IdentityService.BusinessLogic.KafkaMessageBroker;
using Microsoft.Extensions.Options;

namespace IdentityService.API.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string bootstrapServer = configuration["BootstrapServer"]!;

            IOptions<KafkaOptions> options = Options.Create(new KafkaOptions
            {
                BootstrapServer = bootstrapServer,
                Acks = Acks.All,
            });

            services.AddSingleton(options);

            return services;
        }
    }
}

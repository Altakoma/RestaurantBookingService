using Confluent.Kafka;
using IdentityService.BusinessLogic.KafkaMessageBroker;
using IdentityService.DataAccess.Exceptions;
using Microsoft.Extensions.Options;

namespace IdentityService.API.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            var bootstrapServer = configuration["BootstrapServer"];

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer), typeof(string));
            }

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

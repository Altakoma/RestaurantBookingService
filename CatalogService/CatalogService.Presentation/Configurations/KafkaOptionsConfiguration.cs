using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.KafkaMessageBroker;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CatalogService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        private const string BootstrapServerString = "BootstrapServer";

        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? bootstrapServer = Environment.GetEnvironmentVariable(BootstrapServerString) ??
                configuration[BootstrapServerString];

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer),
                    BootstrapServerString, typeof(string));
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

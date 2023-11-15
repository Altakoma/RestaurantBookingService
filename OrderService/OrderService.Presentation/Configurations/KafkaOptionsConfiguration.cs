using Microsoft.Extensions.Options;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.KafkaMessageBroker;

namespace OrderService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        private const string BootstrapServerString = "BootstrapServer";
        private const string GroupNameString = "GroupName";

        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? bootstrapServer = configuration[BootstrapServerString];

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer),
                    typeof(string));
            }

            string? groupName = configuration[GroupNameString];

            if (groupName is null)
            {
                throw new NotFoundException(nameof(groupName),
                    typeof(string));
            }

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

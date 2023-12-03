using Microsoft.Extensions.Options;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.KafkaMessageBroker;

namespace OrderService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            var bootstrapServer = configuration["BootstrapServer"];

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer),
                    typeof(string));
            }

            var groupName = configuration["GroupName"];

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

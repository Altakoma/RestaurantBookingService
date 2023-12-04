using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.KafkaMessageBroker;
using Microsoft.Extensions.Options;

namespace BookingService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string bootstrapServer = configuration[BootstrapServerString]!;

            string groupName = configuration[GroupNameString]!;

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

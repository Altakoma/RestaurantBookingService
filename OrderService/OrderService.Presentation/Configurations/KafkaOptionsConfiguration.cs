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
            string bootstrapServer = configuration.GetConnectionString(BootstrapServerString)!;

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

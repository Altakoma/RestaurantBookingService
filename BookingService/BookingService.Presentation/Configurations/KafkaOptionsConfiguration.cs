using BookingService.Domain.Exceptions;
using CatalogService.Infrastructure.KafkaMessageBroker;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CatalogService.Presentation.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        private const string BootstrapServerString = "BootstrapServer";
        private const string SaslUserNameString = "SaslUserName";
        private const string SaslPasswordString = "SaslPassword";
        private const string GroupNameString = "GroupName";

        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? bootstrapServer = configuration[BootstrapServerString] ??
                Environment.GetEnvironmentVariable(BootstrapServerString);

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer),
                    typeof(string));
            }

            string? userName = configuration[SaslUserNameString] ??
                Environment.GetEnvironmentVariable(SaslUserNameString);

            if (userName is null)
            {
                throw new NotFoundException(nameof(userName),
                    typeof(string));
            }

            string? password = configuration[SaslPasswordString] ??
                Environment.GetEnvironmentVariable(SaslPasswordString);

            if (password is null)
            {
                throw new NotFoundException(nameof(password),
                    typeof(string));
            }

            string? groupName = configuration[GroupNameString] ??
                Environment.GetEnvironmentVariable(GroupNameString);

            if (groupName is null)
            {
                throw new NotFoundException(nameof(groupName),
                    typeof(string));
            }

            IOptions<KafkaOptions> options = Options.Create(new KafkaOptions
            {
                BootstrapServer = bootstrapServer,
                SaslPassword = password,
                SaslUsername = userName,
                GroupName = groupName,
                Acks = Acks.All,
            });

            services.AddSingleton(options);

            return services;
        }
    }
}

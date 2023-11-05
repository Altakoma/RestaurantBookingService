using Confluent.Kafka;
using Google.Protobuf.WellKnownTypes;
using IdentityService.BusinessLogic.KafkaMessageBroker;
using IdentityService.DataAccess.Exceptions;
using Microsoft.Extensions.Options;

namespace IdentityService.API.Configurations
{
    public static class KafkaOptionsConfiguration
    {
        private const string BootstrapServerString = "BootstrapServer";
        private const string SaslUserNameString = "SaslUserName";
        private const string SaslPasswordString = "SaslPassword";

        public static IServiceCollection ConfigureKafkaOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? bootstrapServer = configuration[BootstrapServerString] ??
                Environment.GetEnvironmentVariable(BootstrapServerString);

            if (bootstrapServer is null)
            {
                throw new NotFoundException(nameof(bootstrapServer), typeof(string));
            }

            string? userName = configuration[SaslUserNameString] ??
                Environment.GetEnvironmentVariable(SaslUserNameString);

            if (userName is null)
            {
                throw new NotFoundException(nameof(userName), typeof(string));
            }

            string? password = configuration[SaslPasswordString] ??
                Environment.GetEnvironmentVariable(SaslPasswordString);

            if (password is null)
            {
                throw new NotFoundException(nameof(password), typeof(string));
            }

            IOptions<KafkaOptions> options = Options.Create(new KafkaOptions
            {
                BootstrapServer = bootstrapServer,
                SaslPassword = password,
                SaslUsername = userName,
                Acks = Acks.All,
            });

            services.AddSingleton(options);

            return services;
        }
    }
}

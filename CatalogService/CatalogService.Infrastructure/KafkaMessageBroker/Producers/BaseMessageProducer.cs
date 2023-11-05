using CatalogService.Application.Interfaces.Kafka.Producers.Base;
using CatalogService.Domain.Exceptions;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure.KafkaMessageBroker.Producers
{
    public abstract class BaseMessageProducer : IBaseMessageProducer
    {
        protected readonly IOptions<KafkaOptions> _options;
        protected readonly IConfiguration _configuration;

        private const SecurityProtocol SaslSecurityProtocol = SecurityProtocol.SaslSsl;

        public BaseMessageProducer(IOptions<KafkaOptions> options,
            IConfiguration configuration)
        {
            _options = options;
            _configuration = configuration;
        }

        public virtual async Task ProduceMessageAsync<T>(T item,
            CancellationToken cancellationToken, string topicName)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _options.Value.BootstrapServer,
                SecurityProtocol = SaslSecurityProtocol,
                SaslUsername = _options.Value.SaslUsername,
                SaslPassword = _options.Value.SaslPassword,
                Acks = _options.Value.Acks,
            };

            var producerBuilder = new ProducerBuilder<Null, T>(config);

            using (var producer = producerBuilder.Build())
            {
                var message = new Message<Null, T>
                {
                    Value = item,
                };

                await producer.ProduceAsync(topicName, message, cancellationToken);
            }
        }

        public string GetTopicNameOrThrow(string configurationName, string environmentName)
        {
            string? topicName = _configuration[configurationName] ??
                Environment.GetEnvironmentVariable(environmentName);

            if (topicName is null)
            {
                throw new NotFoundException(nameof(topicName), configurationName,
                    typeof(string));
            }

            return topicName;
        }
    }
}

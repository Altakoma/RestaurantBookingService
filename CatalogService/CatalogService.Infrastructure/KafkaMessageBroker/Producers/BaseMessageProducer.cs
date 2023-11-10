using CatalogService.Application.Interfaces.Kafka.Producers.Base;
using CatalogService.Application.Serializers;
using CatalogService.Domain.Exceptions;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace CatalogService.Infrastructure.KafkaMessageBroker.Producers
{
    public abstract class BaseMessageProducer : IBaseMessageProducer
    {
        protected readonly IOptions<KafkaOptions> _options;
        protected readonly IConfiguration _configuration;

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
                Acks = _options.Value.Acks,
            };

            var producerBuilder = new ProducerBuilder<Null, T>(config);

            producerBuilder.SetValueSerializer(new JsonKafkaSerializer<T>());

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
            string? topicName = Environment.GetEnvironmentVariable(environmentName) ??
                                _configuration[configurationName];

            if (topicName is null)
            {
                throw new NotFoundException(nameof(topicName), configurationName,
                    typeof(string));
            }

            return topicName;
        }
    }
}

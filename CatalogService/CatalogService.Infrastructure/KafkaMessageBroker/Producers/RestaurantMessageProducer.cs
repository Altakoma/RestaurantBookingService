using CatalogService.Application.Interfaces.Kafka.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure.KafkaMessageBroker.Producers
{
    public class RestaurantMessageProducer : BaseMessageProducer, IRestaurantMessageProducer
    {
        private const string TopicNameConfigurationString = "RestaurantTopic";
        private const string TopicNameEnvironmentString = "RestaurantTopic";

        public RestaurantMessageProducer(IOptions<KafkaOptions> options,
            IConfiguration configuration) : base(options, configuration)
        {
        }

        public async Task ProduceMessageAsync<T>(T item,
            CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            await ProduceMessageAsync<T>(item, cancellationToken, topicName);
        }
    }
}

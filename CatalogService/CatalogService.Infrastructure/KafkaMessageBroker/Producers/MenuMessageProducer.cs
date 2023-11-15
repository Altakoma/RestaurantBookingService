using CatalogService.Application.Interfaces.Kafka.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure.KafkaMessageBroker.Producers
{
    public class MenuMessageProducer : BaseMessageProducer, IMenuMessageProducer
    {
        private const string TopicNameString = "MenuTopic";

        public MenuMessageProducer(IOptions<KafkaOptions> options,
            IConfiguration configuration) : base(options, configuration)
        {
        }

        public async Task ProduceMessageAsync<T>(T item,
            CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameString);

            await ProduceMessageAsync<T>(item, cancellationToken, topicName);
        }
    }
}

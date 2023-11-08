using AutoMapper;
using BookingService.Application.DTOs.Client.Messages;
using BookingService.Application.Interfaces.Kafka.Consumers;
using BookingService.Domain.Entities;
using CatalogService.Infrastructure.KafkaMessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BookingService.Infrastructure.KafkaMessageBroker.Consumers
{
    public class ClientMessageConsumer
        : BaseMessageConsumer<InsertClientMessageDTO, UpdateClientMessageDTO, Client>,
        IClientMessageConsumer
    {
        private const string TopicNameConfigurationString = "UserTopic";
        private const string TopicNameEnvironmentString = "UserTopic";

        public ClientMessageConsumer(IOptions<KafkaOptions> options,
            IConfiguration configuration, IServiceProvider serviceProvider,
            IMapper mapper) : base(serviceProvider, options, configuration, mapper)
        {
        }

        public async Task HandleConsumingMessages(CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            await ConsumeMessage(cancellationToken, topicName);
        }
    }
}

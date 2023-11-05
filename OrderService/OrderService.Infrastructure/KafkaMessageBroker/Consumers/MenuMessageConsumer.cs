using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Menu.Messages;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.KafkaMessageBroker.Consumers
{
    public class MenuMessageConsumer
        : BaseMessageConsumer<InsertMenuMessageDTO, UpdateMenuMessageDTO, Menu>,
        IMenuMessageConsumer
    {
        private const string TopicNameConfigurationString = "MenuTopic";
        private const string TopicNameEnvironmentString = "MenuTopic";

        public MenuMessageConsumer(IOptions<KafkaOptions> options,
            IConfiguration configuration, IServiceProvider serviceProvider,
            IMapper mapper) : base(serviceProvider, options, configuration, mapper)
        {
        }

        public Task HandleConsumingMessages(CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            return ConsumeMessage(cancellationToken, topicName);
        }
    }
}

using AutoMapper;
using BookingService.Application.DTOs.Restaurant.Messages;
using BookingService.Application.Interfaces.Kafka.Consumers;
using BookingService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BookingService.Infrastructure.KafkaMessageBroker.Consumers
{
    public class RestaurantMessageConsumer :
        BaseMessageConsumer<InsertRestaurantMessageDTO, UpdateRestaurantMessageDTO, Restaurant>,
        IRestaurantMessageConsumer
    {
        private const string TopicNameString = "RestaurantTopic";

        public RestaurantMessageConsumer(IOptions<KafkaOptions> options,
            IConfiguration configuration, IServiceProvider serviceProvider,
            IMapper mapper) : base(serviceProvider, options, configuration, mapper)
        {
        }

        public async Task HandleConsumingMessages(CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameString);

            await ConsumeMessage(cancellationToken, topicName);
        }
    }
}

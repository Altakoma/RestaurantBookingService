using BookingService.Application.Interfaces.Kafka.Consumers;
using Microsoft.Extensions.Hosting;

namespace BookingService.Application.Services.Background
{
    public class RestaurantConsumingMessagesHandlingService : BackgroundService
    {
        private readonly IRestaurantMessageConsumer _restaurantMessageConsumer;

        public RestaurantConsumingMessagesHandlingService(
            IRestaurantMessageConsumer restaurantMessageConsumer)
        {
            _restaurantMessageConsumer = restaurantMessageConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _restaurantMessageConsumer.HandleConsumingMessages(stoppingToken);
        }
    }
}

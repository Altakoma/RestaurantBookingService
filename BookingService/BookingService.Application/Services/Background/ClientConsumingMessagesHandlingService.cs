using BookingService.Application.Interfaces.Kafka.Consumers;
using Microsoft.Extensions.Hosting;

namespace BookingService.Application.Services.Background
{
    public class ClientConsumingMessagesHandlingService : BackgroundService
    {
        private readonly IClientMessageConsumer _clientMessageConsumer;

        public ClientConsumingMessagesHandlingService(
            IClientMessageConsumer clientMessageConsumer)
        {
            _clientMessageConsumer = clientMessageConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _clientMessageConsumer.HandleConsumingMessagesAsync(stoppingToken);
        }
    }
}

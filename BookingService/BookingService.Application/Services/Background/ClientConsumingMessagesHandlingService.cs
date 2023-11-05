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

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _clientMessageConsumer.HandleConsumingMessages(stoppingToken);
        }
    }
}

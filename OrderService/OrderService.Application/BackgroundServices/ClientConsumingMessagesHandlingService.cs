using Microsoft.Extensions.Hosting;
using OrderService.Application.Interfaces.Kafka.Consumers;

namespace OrderService.Application.BackgroundServices
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

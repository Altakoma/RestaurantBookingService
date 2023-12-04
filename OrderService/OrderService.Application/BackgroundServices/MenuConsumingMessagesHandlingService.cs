using Microsoft.Extensions.Hosting;
using OrderService.Application.Interfaces.Kafka.Consumers;

namespace OrderService.Application.BackgroundServices
{
    public class MenuConsumingMessagesHandlingService : BackgroundService
    {
        private readonly IMenuMessageConsumer _menuMessageConsumer;

        public MenuConsumingMessagesHandlingService(
            IMenuMessageConsumer menuMessageConsumer)
        {
            _menuMessageConsumer = menuMessageConsumer;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _menuMessageConsumer.HandleConsumingMessagesAsync(stoppingToken);
        }
    }
}

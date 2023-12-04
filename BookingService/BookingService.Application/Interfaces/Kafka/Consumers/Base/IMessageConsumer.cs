namespace BookingService.Application.Interfaces.Kafka.Consumers.Base
{
    public interface IMessageConsumer : IBaseMessageConsumer
    {
        Task HandleConsumingMessagesAsync(CancellationToken cancellationToken);
    }
}

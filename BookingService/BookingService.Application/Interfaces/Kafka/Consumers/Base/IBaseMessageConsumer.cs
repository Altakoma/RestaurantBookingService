namespace BookingService.Application.Interfaces.Kafka.Consumers.Base
{
    public interface IBaseMessageConsumer
    {
        Task ConsumeMessageAsync(CancellationToken cancellationToken, string topicName);
    }
}

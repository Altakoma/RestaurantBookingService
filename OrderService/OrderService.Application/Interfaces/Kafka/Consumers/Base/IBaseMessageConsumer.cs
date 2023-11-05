namespace OrderService.Application.Interfaces.Kafka.Consumers.Base
{
    public interface IBaseMessageConsumer
    {
        Task ConsumeMessage(CancellationToken cancellationToken, string topicName);
    }
}

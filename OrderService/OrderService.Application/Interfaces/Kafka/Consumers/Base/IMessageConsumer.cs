namespace OrderService.Application.Interfaces.Kafka.Consumers.Base
{
    public interface IMessageConsumer : IBaseMessageConsumer
    {
        Task HandleConsumingMessages(CancellationToken cancellationToken);
    }
}

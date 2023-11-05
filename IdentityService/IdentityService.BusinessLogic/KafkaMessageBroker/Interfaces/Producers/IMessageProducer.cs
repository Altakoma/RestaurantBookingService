namespace IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers
{
    public interface IMessageProducer : IBaseMessageProducer
    {
        Task ProduceMessageAsync<T>(T item, CancellationToken cancellationToken);
    }
}

namespace IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers
{
    public interface IBaseMessageProducer
    {
        Task ProduceMessageAsync<T>(T item, CancellationToken cancellationToken, string topicName);
    }
}

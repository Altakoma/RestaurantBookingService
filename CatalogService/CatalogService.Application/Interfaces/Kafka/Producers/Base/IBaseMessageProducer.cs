namespace CatalogService.Application.Interfaces.Kafka.Producers.Base
{
    public interface IBaseMessageProducer
    {
        Task ProduceMessageAsync<T>(T item, CancellationToken cancellationToken, string topicName);
    }
}

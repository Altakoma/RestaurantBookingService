namespace CatalogService.Application.Interfaces.Kafka.Producers.Base
{
    public interface IMessageProducer : IBaseMessageProducer
    {
        Task ProduceMessageAsync<T>(T item, CancellationToken cancellationToken);
    }
}

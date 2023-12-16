using CatalogService.Application.Interfaces.Kafka.Producers.Base;
using Moq;

namespace CatalogService.Tests.Mocks.MessageProducers
{
    public class BaseMessageProducerMock<TMessageProducer> : Mock<TMessageProducer>
        where TMessageProducer : class, IMessageProducer
    {
        public BaseMessageProducerMock<TMessageProducer> MockProduceMessageAsync<T>(T messageDTO)
        {
            Setup(menuMessageProducer => menuMessageProducer.ProduceMessageAsync(
                messageDTO, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public BaseMessageProducerMock<TMessageProducer> MockProduceMessageAsync<T>()
        {
            Setup(menuMessageProducer => menuMessageProducer.ProduceMessageAsync(
                It.IsAny<T>(), It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}

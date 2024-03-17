using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using IdentityService.API.Tests.Mocks.Repositories;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using IdentityService.BusinessLogic.KafkaMessageBroker.Producers;
using Moq;

namespace IdentityService.API.Tests.Mocks.Producers
{
    public class UserMessageProducerMock : Mock<IUserMessageProducer>
    {
        public UserMessageProducerMock MockProduceMessageAsync<T>(T message)
        {
            Setup(userMessageProducer => userMessageProducer.ProduceMessageAsync(It.Is<T>(
                currentMessage => currentMessage.Should().BeEquivalentTo(message, string.Empty) != null),
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}

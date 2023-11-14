using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using System.Text.Json;

namespace OrderService.Infrastructure.KafkaMessageBroker.Consumers
{
    public class ClientMessageConsumer
        : BaseMessageConsumer<InsertClientMessageDTO, UpdateClientMessageDTO, Client>,
        IClientMessageConsumer
    {
        private const string TopicNameConfigurationString = "UserTopic";
        private const string TopicNameEnvironmentString = "UserTopic";

        public ClientMessageConsumer(IOptions<KafkaOptions> options,
            IConfiguration configuration, IServiceProvider serviceProvider,
            IMapper mapper)
            : base(serviceProvider, options, configuration, mapper)
        {
        }

        public async Task HandleConsumingMessages(CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            await ConsumeMessage(cancellationToken, topicName);
        }

        protected override async Task DeleteAsync(string message,
            ISqlRepository<Client> repository, CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<DeleteMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(DeleteMessageDTO));
            }

            var client = await repository.GetByIdAsync<Client>(messageDTO.Id, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (var order in client.Orders)
                {
                    var command = _mapper.Map<DeleteOrderCommand>(order);
                    command.IsRequestedBySystem = true;
                    command.IsTransactionSkipped = true;

                    await mediator.Send(command);
                }

                var clientCommand = _mapper.Map<DeleteClientCommand>(client);
                clientCommand.IsTransactionSkipped = true;

                await mediator.Send(clientCommand);
            }
        }

        protected override async Task UpdateAsync(string message,
            ISqlRepository<Client> repository, CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<UpdateClientMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(UpdateClientMessageDTO));
            }

            var client = await repository.GetByIdAsync<Client>(messageDTO.Id, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var clientCommand = _mapper.Map<UpdateClientCommand>(client);
                clientCommand.IsTransactionSkipped = true;

                ReadClientDTO readClientDTO = await mediator.Send(clientCommand);

                foreach (var order in client.Orders)
                {
                    var deleteCommand = _mapper.Map<DeleteOrderCommand>(order);
                    deleteCommand.IsRequestedBySystem = true;
                    deleteCommand.IsTransactionSkipped = true;

                    await mediator.Send(deleteCommand);

                    var insertCommand = _mapper.Map<InsertOrderCommand>(order);
                    insertCommand.IsRequestedBySystem = true;
                    insertCommand.IsTransactionSkipped = true;

                    await mediator.Send(insertCommand);
                }
            }
        }
    }
}

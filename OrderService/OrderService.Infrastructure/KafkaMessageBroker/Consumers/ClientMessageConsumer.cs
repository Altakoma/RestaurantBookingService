﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Entities;
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

            await base.DeleteAsync(message, repository, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                var noSqlRepository = scope.ServiceProvider
                                           .GetRequiredService<INoSqlOrderRepository>();

                await noSqlRepository.DeleteOrdersByClientIdAsync(messageDTO!.Id, cancellationToken);
            }
        }

        protected override async Task<Client> UpdateAsync(string message,
            ISqlRepository<Client> repository, CancellationToken cancellationToken)
        {
            Client client = await base.UpdateAsync(message, repository, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (var order in client.Orders)
                {
                    var updateCommand = _mapper.Map<UpdateOrderBySynchronizationCommand>(order);

                    await mediator.Send(updateCommand);
                }
            }

            return client;
        }
    }
}

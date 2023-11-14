using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Application.DTOs.Menu.Messages;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using System.Text.Json;

namespace OrderService.Infrastructure.KafkaMessageBroker.Consumers
{
    public class MenuMessageConsumer
        : BaseMessageConsumer<InsertMenuMessageDTO, UpdateMenuMessageDTO, Menu>,
        IMenuMessageConsumer
    {
        private const string TopicNameConfigurationString = "MenuTopic";
        private const string TopicNameEnvironmentString = "MenuTopic";

        public MenuMessageConsumer(IOptions<KafkaOptions> options,
            IConfiguration configuration, IServiceProvider serviceProvider,
            IMapper mapper) : base(serviceProvider, options, configuration, mapper)
        {
        }

        public async Task HandleConsumingMessages(CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            await ConsumeMessage(cancellationToken, topicName);
        }

        protected override async Task DeleteAsync(string message,
            ISqlRepository<Menu> repository, CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<DeleteMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(DeleteMessageDTO));
            }

            var menu = await repository.GetByIdAsync<Menu>(messageDTO.Id, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (var order in menu.Orders)
                {
                    var command = _mapper.Map<DeleteOrderCommand>(order);
                    command.IsRequestedBySystem = true;
                    command.IsTransactionSkipped = true;

                    await mediator.Send(command);
                }

                var menuCommand = _mapper.Map<DeleteMenuCommand>(menu);
                menuCommand.IsTransactionSkipped = true;

                await mediator.Send(menuCommand);
            }
        }

        protected override async Task UpdateAsync(string message,
            ISqlRepository<Menu> repository, CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<UpdateMenuMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(UpdateMenuMessageDTO));
            }

            var menu = await repository.GetByIdAsync<Menu>(messageDTO.Id, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                foreach (var order in menu.Orders)
                {
                    var command = _mapper.Map<UpdateOrderCommand>(order);
                    command.IsRequestedBySystem = true;
                    command.IsTransactionSkipped = true;

                    await mediator.Send(command);
                }

                var menuCommand = _mapper.Map<UpdateMenuCommand>(menu);
                menuCommand.IsTransactionSkipped = true;

                await mediator.Send(menuCommand);
            }
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.DTOs.Menu.Messages;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;
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

            await base.DeleteAsync(message, repository, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                var noSqlRepository = scope.ServiceProvider
                                           .GetRequiredService<INoSqlOrderRepository>();

                await noSqlRepository.DeleteOrderByMenuIdAsync(messageDTO!.Id, cancellationToken);
            }
        }

        protected override async Task<Menu> UpdateAsync(string message,
            ISqlRepository<Menu> repository, CancellationToken cancellationToken)
        {
            Menu menu = await base.UpdateAsync(message, repository, cancellationToken);

            using (var scope = _services.CreateScope())
            {
                var SqlRepository = scope.ServiceProvider
                                           .GetRequiredService<ISqlOrderRepository>();

                var noSqlRepository = scope.ServiceProvider
                                           .GetRequiredService<INoSqlOrderRepository>();

                foreach (var order in menu.Orders)
                {
                    var readOrderDTO = await SqlRepository
                    .GetByIdAsync<ReadOrderDTO>(order.Id, cancellationToken);

                    await noSqlRepository.UpdateAsync(readOrderDTO, cancellationToken);
                }
            }

            return menu;
        }
    }
}

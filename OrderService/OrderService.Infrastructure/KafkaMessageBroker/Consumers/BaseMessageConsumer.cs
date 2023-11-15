using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.Interfaces.Kafka.Consumers.Base;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Exceptions;
using System.Text.Json;

namespace OrderService.Infrastructure.KafkaMessageBroker.Consumers
{
    public abstract class BaseMessageConsumer<InsertMessage, UpdateMessage, Initial>
        : IBaseMessageConsumer
    {
        protected readonly IServiceProvider _services;

        protected readonly IOptions<KafkaOptions> _options;
        protected readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;

        public BaseMessageConsumer(IServiceProvider services,
            IOptions<KafkaOptions> options,
            IConfiguration configuration, IMapper mapper)
        {
            _options = options;
            _configuration = configuration;
            _mapper = mapper;
            _services = services;
        }

        public async Task ConsumeMessage(CancellationToken cancellationToken,
            string topicName)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _options.Value.BootstrapServer,
                AllowAutoCreateTopics = true,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _options.Value.GroupName,
                EnableAutoCommit = false,
            };

            var consumerBuilder = new ConsumerBuilder<Null, string>(config);

            string message;

            using (var consumer = consumerBuilder.Build())
            {
                consumer.Subscribe(topicName);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        ConsumeResult<Null, string> result =
                        await Task.Run(() => consumer.Consume(cancellationToken));

                        message = result.Message.Value;

                        MessageDTO? messageDTO = JsonSerializer
                                                 .Deserialize<MessageDTO>(message);

                        if (messageDTO is not null)
                        {
                            await HandleMessage(messageDTO.Type, message,
                                                cancellationToken);
                        }

                        consumer.Commit();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public async Task HandleMessage(MessageType type, string message,
            CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var repository = scope.ServiceProvider
                                      .GetRequiredService<ISqlRepository<Initial>>();

                switch (type)
                {
                    case MessageType.Delete:
                        await DeleteAsync(message, repository, cancellationToken);
                        break;
                    case MessageType.Insert:
                        await InsertAsync(message, repository, cancellationToken);
                        break;
                    case MessageType.Update:
                        await UpdateAsync(message, repository, cancellationToken);
                        break;
                    default:
                        return;
                }
            }
        }

        protected virtual async Task DeleteAsync(string message, ISqlRepository<Initial> repository,
            CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<DeleteMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(DeleteMessageDTO));
            }

            await repository.DeleteAsync(messageDTO.Id, cancellationToken);
            await repository.SaveChangesToDbAsync(cancellationToken);
        }

        protected virtual async Task<Initial> UpdateAsync(string message,
            ISqlRepository<Initial> repository, CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<UpdateMessage>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(UpdateMessage));
            }

            var restaurant = _mapper.Map<Initial>(messageDTO);

            return await repository.UpdateAsync<Initial>(restaurant, cancellationToken);
        }

        protected virtual async Task InsertAsync(string message, ISqlRepository<Initial> repository,
            CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<InsertMessage>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(InsertMessage));
            }

            var restaurant = _mapper.Map<Initial>(messageDTO);

            await repository.InsertAsync<Initial>(restaurant, cancellationToken);
        }

        public string GetTopicNameOrThrow(string configurationName)
        {
            string? topicName = _configuration[configurationName];

            if (topicName is null)
            {
                throw new NotFoundException(nameof(topicName), typeof(string));
            }

            return topicName;
        }
    }
}

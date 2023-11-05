using AutoMapper;
using BookingService.Application.DTOs.Base.Messages;
using BookingService.Application.Interfaces.Kafka.Consumers.Base;
using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Exceptions;
using CatalogService.Infrastructure.KafkaMessageBroker;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BookingService.Infrastructure.KafkaMessageBroker.Consumers
{
    public abstract class BaseMessageConsumer<InsertMessage, UpdateMessage, Initial> 
        : IBaseMessageConsumer
    {
        protected readonly IOptions<KafkaOptions> _options;
        protected readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;

        private const SecurityProtocol SaslSecurityProtocol = SecurityProtocol.SaslSsl;

        private readonly IServiceProvider _services;

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
                SecurityProtocol = SaslSecurityProtocol,
                SaslUsername = _options.Value.SaslUsername,
                SaslPassword = _options.Value.SaslPassword,
                GroupId = _options.Value.GroupName,
                Acks = _options.Value.Acks,
            };

            var consumerBuilder = new ConsumerBuilder<Null, string>(config);

            string message;

            using (var consumer = consumerBuilder.Build())
            {
                consumer.Subscribe(topicName);

                while (!cancellationToken.IsCancellationRequested)
                {
                    ConsumeResult<Null, string> result = consumer
                                                         .Consume(cancellationToken);

                    message = result.Message.Value;

                    MessageDTO? messageDTO = JsonSerializer
                                             .Deserialize<MessageDTO>(message);

                    if (messageDTO is not null)
                    {
                        await HandleMessage(messageDTO.Type, message,
                                            cancellationToken);
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
                                      .GetRequiredService<IRepository<Initial>>();

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

        private async Task DeleteAsync(string message, IRepository<Initial> repository,
            CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<DeleteMessageDTO>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(DeleteMessageDTO));
            }

            await repository.DeleteAsync(messageDTO.Id, cancellationToken);
        }

        private async Task UpdateAsync(string message, IRepository<Initial> repository,
            CancellationToken cancellationToken)
        {
            var messageDTO = JsonSerializer.Deserialize<UpdateMessage>(message);

            if (messageDTO is null)
            {
                throw new NotFoundException(message, typeof(UpdateMessage));
            }

            var restaurant = _mapper.Map<Initial>(messageDTO);

            await repository.UpdateAsync<Initial>(restaurant, cancellationToken);
        }

        private async Task InsertAsync(string message, IRepository<Initial> repository,
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

        public string GetTopicNameOrThrow(string configurationName, string environmentName)
        {
            string? topicName = _configuration[configurationName] ??
                Environment.GetEnvironmentVariable(environmentName);

            if (topicName is null)
            {
                throw new NotFoundException(nameof(topicName), typeof(string));
            }

            return topicName;
        }
    }
}

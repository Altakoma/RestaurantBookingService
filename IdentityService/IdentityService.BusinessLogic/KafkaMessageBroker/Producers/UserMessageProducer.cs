﻿using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IdentityService.BusinessLogic.KafkaMessageBroker.Producers
{
    public class UserMessageProducer : BaseMessageProducer, IMessageProducer
    {
        private const string TopicNameConfigurationString = "UserTopic";
        private const string TopicNameEnvironmentString = "UserTopic";

        public UserMessageProducer(IOptions<KafkaOptions> options,
            IConfiguration configuration) : base(options, configuration)
        {
        }

        public async Task ProduceMessageAsync<T>(T item,
            CancellationToken cancellationToken)
        {
            string topicName = GetTopicNameOrThrow(TopicNameConfigurationString,
                TopicNameEnvironmentString);

            await ProduceMessageAsync<T>(item, cancellationToken, topicName);
        }
    }
}

using Confluent.Kafka;

namespace OrderService.Infrastructure.KafkaMessageBroker
{
    public class KafkaOptions
    {
        public string BootstrapServer { get; set; } = null!;
        public string GroupName { get; set; } = null!;
    }
}

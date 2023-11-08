using Confluent.Kafka;

namespace CatalogService.Infrastructure.KafkaMessageBroker
{
    public class KafkaOptions
    {
        public string BootstrapServer { get; set; } = null!;
        public string GroupName { get; set; } = null!;
    }
}

using Confluent.Kafka;

namespace CatalogService.Infrastructure.KafkaMessageBroker
{
    public class KafkaOptions
    {
        public string BootstrapServer { get; set; } = null!;
        public Acks Acks { get; set; }
    }
}

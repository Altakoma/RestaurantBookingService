using Confluent.Kafka;

namespace IdentityService.BusinessLogic.KafkaMessageBroker
{
    public class KafkaOptions
    {
        public string BootstrapServer { get; set; } = null!;
        public string SaslUsername { get; set; } = null!;
        public string SaslPassword { get; set; } = null!;
        public Acks Acks { get; set; }
    }
}

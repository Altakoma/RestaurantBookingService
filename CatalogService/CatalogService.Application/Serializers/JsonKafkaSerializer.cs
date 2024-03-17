using Confluent.Kafka;
using System.Text.Json;

namespace CatalogService.Application.Serializers
{
    public class JsonKafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            using (var ms = new MemoryStream())
            {
                string jsonString = JsonSerializer.Serialize(data);
                var writer = new StreamWriter(ms);

                writer.Write(jsonString);
                writer.Flush();
                ms.Position = 0;

                return ms.ToArray();
            }
        }
    }
}

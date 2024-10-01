using Confluent.Kafka;
using Silverback.Messaging.Configuration.Kafka;

namespace Eximia.CsharpCourse.SeedWork.Kafka;

public class KafkaConfig
{
    public KafkaConfig()
    {
        Connection = new KafkaConnectionConfig(string.Empty, Guid.NewGuid().ToString(), 
            SecurityProtocol.Plaintext, string.Empty, string.Empty);
        Consumer = new KafkaConsumerConfig(Array.Empty<KafkaInboundConfig>().ToList());
    }
    public KafkaConnectionConfig Connection { get; set; }
    public KafkaConsumerConfig Consumer { get; set; }
}

public record KafkaConnectionConfig(string BootstrapServers, string GroupId, SecurityProtocol SecurityProtocol, string Username, string Password);

public record KafkaConsumerConfig(List<KafkaInboundConfig> Inbounds);

public record KafkaInboundConfig(string Topics, int AutoOffsetReset);

public static class KafkaClientConfigExtensions
{
    public static void Configure(this KafkaClientConfig kafka, KafkaConfig config)
    {
        switch (config.Connection.SecurityProtocol)
        {
            case SecurityProtocol.Plaintext:
                kafka.ConfigurePlainText(config);
                break;
            case SecurityProtocol.Ssl:
                throw new Exception("Invalid security protocol Ssl");
            case SecurityProtocol.SaslPlaintext:
                throw new Exception("Invalid security protocol Sasl Plain Text");
            case SecurityProtocol.SaslSsl:
                throw new Exception("Invalid security protocol SaslSsl");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void ConfigurePlainText(this KafkaClientConfig kafka, KafkaConfig config)
    {
        if (config.Connection.SecurityProtocol != SecurityProtocol.Plaintext)
            throw new Exception("Invalid security protocol configuration for plain text");
        kafka.BootstrapServers = config.Connection.BootstrapServers;
        kafka.SecurityProtocol = SecurityProtocol.Plaintext;
    }
}
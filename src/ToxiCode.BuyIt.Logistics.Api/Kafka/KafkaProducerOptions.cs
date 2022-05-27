using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;

namespace ToxiCode.BuyIt.Logistics.Api.Kafka;

public class KafkaProducerOptions
{
    [Required]
    public string TopicName { get; init; } = null!;

    [Required]
    public ProducerConfig ProducerConfig { get; init; } = null!;
}

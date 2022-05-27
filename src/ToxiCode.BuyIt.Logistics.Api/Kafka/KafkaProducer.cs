using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace ToxiCode.BuyIt.Logistics.Api.Kafka;

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _kafkaProducer;
    private readonly ILogger<KafkaProducer> _logger;
    private readonly string _topicName;

    public KafkaProducer(
        IOptions<KafkaProducerOptions> config,
        ILogger<KafkaProducer> logger
    )
    {
        if (config == null)
            throw new ArgumentNullException(
                $"Producer creation exception. {nameof(KafkaProducerOptions)} has no value");

        _logger = logger;
        _topicName = config.Value.TopicName;
        _kafkaProducer = CreateProducer(config.Value?.ProducerConfig!);
    }

    public void Dispose()
        => _kafkaProducer.Dispose();

    public async Task SendMessageAsync(string key, string message, CancellationToken cancellationToken)
    {
        await _kafkaProducer.ProduceAsync(_topicName,
            new Message<string, string>
            {
                Key = key,
                Value = message
            }, cancellationToken);
    }

    private IProducer<string, string> CreateProducer(ProducerConfig config)
    {
        var builder = new ProducerBuilder<string, string>(config)
            .SetLogHandler((_, message) =>
                _logger.LogError("Librdkafka message: {Name}:{Message}", message.Name, message.Message))
            .SetErrorHandler((_, error) => DefaultErrorHandler(error));

        return builder.Build();
    }

    private void DefaultErrorHandler(Error error)
        => _logger.LogError("{IsFatalError} error {ErrorCode} occured in confluent kafka: {ErrorReason}",
            error.IsFatal ? "Fatal" : "Non-fatal",
            error.Code,
            error.Reason);
}
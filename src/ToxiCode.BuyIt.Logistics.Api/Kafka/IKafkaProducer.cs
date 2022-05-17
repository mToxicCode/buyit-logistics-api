namespace ToxiCode.BuyIt.Logistics.Api.Kafka;

public interface IKafkaProducer
{
    public Task SendMessageAsync(string key, string message, CancellationToken cancellationToken);
}

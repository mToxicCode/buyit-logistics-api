using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;
using ToxiCode.BuyIt.Logistics.Api.Kafka;

namespace ToxiCode.BuyIt.Logistics.Api.Infrustructure.Extensions;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<KafkaProducerOptions>()
            .Configure(options => configuration.GetSection(nameof(KafkaProducerOptions)).Bind(options))
            .ValidateDataAnnotations();

        return services
            .AddSingleton<IKafkaProducer, KafkaProducer>().
            AddSingleton<OrdersServiceNotificator>();
    }

}
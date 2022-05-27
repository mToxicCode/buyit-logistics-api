using System.Text.Json;
using Confluent.Kafka;
using Dtos;
using Newtonsoft.Json;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.Kafka;
using ToxiCode.BuyIt.Api.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using ToxiCode.BuyIt.Logistics.Api.Grpc;
using Action = ToxiCode.BuyIt.Api.Contracts.Action;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;

public class OrdersServiceNotificator
{
    private readonly IKafkaProducer _producer;

    public OrdersServiceNotificator(IKafkaProducer producer)
        => _producer = producer;

    public Task NotifyStatusChanged(long orderId, OrderStatus status, CancellationToken cancellationToken)
    {
        var request = new OrderStatusChangedNotificationMessage
        {
            OrderId = orderId,
            OrderStatus = status
        };
        var json = JsonConvert.SerializeObject(request);
        return _producer.SendMessageAsync(orderId.ToString(), json, cancellationToken);
    }
}
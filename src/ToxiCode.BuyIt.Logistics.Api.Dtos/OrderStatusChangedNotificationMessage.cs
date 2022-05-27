using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace Dtos;

public class OrderStatusChangedNotificationMessage
{
    public long OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;

public class SetOrderStatusQuery
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
}
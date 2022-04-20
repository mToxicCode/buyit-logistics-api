using Dtos.Order;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;

public class GetOrdersResponse
{
    public IEnumerable<Order>? Orders { get; set; }
}
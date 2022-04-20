using Dtos.Order;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;

public class GetOrderResponse
{
    public Order? Order { get; set; }
}
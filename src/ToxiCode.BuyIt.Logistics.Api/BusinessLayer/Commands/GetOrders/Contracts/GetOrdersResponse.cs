using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public class GetOrdersResponse
{
    public OrderDto[] Orders { get; set; } = null!;
}
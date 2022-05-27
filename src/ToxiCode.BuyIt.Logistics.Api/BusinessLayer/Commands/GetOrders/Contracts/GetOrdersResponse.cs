using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public class GetOrdersResponse
{
    public IEnumerable<OrderDto>? Orders { get; set; }
}
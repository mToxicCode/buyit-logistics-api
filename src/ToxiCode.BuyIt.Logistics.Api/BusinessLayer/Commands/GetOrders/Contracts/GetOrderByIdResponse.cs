using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public class GetOrderByIdResponse
{
    public OrderDto Orders { get; set; } = null!;
}
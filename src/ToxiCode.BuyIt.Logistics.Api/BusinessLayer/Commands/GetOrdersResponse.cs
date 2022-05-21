using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;

public class GetOrdersResponse
{
    public IEnumerable<Order>? Orders { get; set; }
}
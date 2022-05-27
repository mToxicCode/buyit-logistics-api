using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public class GetOrdersByBuyerIdCommand : IRequest<GetOrdersByBuyerIdResponse>
{
    public string BuyerId { get; set; } = null!;
}
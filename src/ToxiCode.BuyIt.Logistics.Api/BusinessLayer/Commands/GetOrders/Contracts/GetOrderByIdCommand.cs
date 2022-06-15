using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public class GetOrderByIdCommand : IRequest<GetOrderByIdResponse>
{
    public long OrderId { get; set; }
}
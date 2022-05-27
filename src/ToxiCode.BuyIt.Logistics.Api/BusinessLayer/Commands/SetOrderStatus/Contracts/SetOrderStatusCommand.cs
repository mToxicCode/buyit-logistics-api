using MediatR;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.SetOrderStatus.Contracts;

public class SetOrderStatusCommand : IRequest
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
}
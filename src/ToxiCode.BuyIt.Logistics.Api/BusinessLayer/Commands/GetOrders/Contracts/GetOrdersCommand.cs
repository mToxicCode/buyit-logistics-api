using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

public record class GetOrdersCommand : IRequest<GetOrdersResponse>;
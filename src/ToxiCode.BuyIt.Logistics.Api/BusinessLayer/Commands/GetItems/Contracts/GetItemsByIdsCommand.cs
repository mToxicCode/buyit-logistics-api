using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;

public class GetItemsByIdsCommand : IRequest<GetItemsResponse>
{
    public IEnumerable<long> ItemIds { get; set; } = null!;
}
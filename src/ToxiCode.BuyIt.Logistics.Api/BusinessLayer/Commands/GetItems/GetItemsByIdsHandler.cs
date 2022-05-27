using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems;

public class GetItemsByIdsHandler : IRequestHandler<GetItemsByIdsCommand, GetItemsResponse>
{
    private readonly ItemsRepository _itemsRepository;

    public GetItemsByIdsHandler(ItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public async Task<GetItemsResponse> Handle(GetItemsByIdsCommand request, CancellationToken cancellationToken)
    {
        var response = new GetItemsResponse()
        {
            Items = await _itemsRepository.GetItemsByIds(new GetItemsByIdsQuery
            {
                Ids = request.ItemIds.ToArray()
            })
        };
        return response;
    }
   
}
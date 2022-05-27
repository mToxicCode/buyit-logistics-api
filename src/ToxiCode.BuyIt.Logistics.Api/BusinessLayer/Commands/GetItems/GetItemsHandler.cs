    using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems;

public class GetItemsHandler : IRequestHandler<GetItemsCommand, GetItemsResponse>
{
    private readonly ItemsRepository _itemsRepository;

    public GetItemsHandler(ItemsRepository itemsRepository) => _itemsRepository = itemsRepository;

    public async Task<GetItemsResponse> Handle(GetItemsCommand request, CancellationToken cancellationToken)
    {
        return  new GetItemsResponse
        {
            Items = await _itemsRepository.GetItems()
        };
    }
}
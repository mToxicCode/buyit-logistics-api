using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem;

public class CreateItemsHandler : IRequestHandler<CreateItemRequest, CreateItemResponse>
{
    private readonly ItemsRepository _itemsRepository;

    public CreateItemsHandler(ItemsRepository itemsRepository) 
        => _itemsRepository = itemsRepository;

    public async Task<CreateItemResponse> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        return await _itemsRepository.CreateItem(request, cancellationToken);
    }
}
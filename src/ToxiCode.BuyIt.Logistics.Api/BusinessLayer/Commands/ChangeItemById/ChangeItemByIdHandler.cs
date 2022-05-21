using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById;

public class ChangeItemByIdHandler : AsyncRequestHandler<ChangeItemRequest>
{
    private readonly ItemsRepository _itemsRepository;

    public ChangeItemByIdHandler(ItemsRepository itemsRepository) 
        => _itemsRepository = itemsRepository;

    protected override async Task Handle(ChangeItemRequest request, CancellationToken cancellationToken)
    {
        await _itemsRepository.ChangeItem(request, cancellationToken);
    }
}
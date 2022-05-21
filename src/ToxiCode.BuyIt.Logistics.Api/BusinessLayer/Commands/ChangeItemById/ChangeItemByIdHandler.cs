using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById;

public class ChangeItemByIdHandler : AsyncRequestHandler<ChangeItemCommand>
{
    private readonly ItemsRepository _itemsRepository;

    public ChangeItemByIdHandler(ItemsRepository itemsRepository) 
        => _itemsRepository = itemsRepository;

    protected override async Task Handle(ChangeItemCommand command, CancellationToken cancellationToken)
    {
        var query = new ChangeItemQuery
        {
            Id = command.Id,
            ItemName = command.ItemName,
            Weight = command.Weight,
            Height = command.Height,
            Length = command.Length,
            Width = command.Width,
        };
        await _itemsRepository.ChangeItem(query, cancellationToken);
    }
}
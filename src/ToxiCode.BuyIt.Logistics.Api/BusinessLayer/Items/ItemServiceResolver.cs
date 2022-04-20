using Dtos.Items;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items;

public class ItemServiceResolver : IItemService
{
    private readonly ItemsRepository _repository;

    public ItemServiceResolver(ItemsRepository repository) => _repository = repository;

    public Task<long> CreateItem(CreateItemRequest request, CancellationToken cancellationToken) 
        => _repository.CreateItem(request, cancellationToken);

    public Task DeleteItem(long id, CancellationToken cancellationToken)
        => _repository.DeleteItemById(id, cancellationToken);

    public Task ChangeItem(ChangeItemRequest request, CancellationToken cancellationToken)
        => _repository.ChangeItem(request, cancellationToken);

    public async Task<GetItemResponse?> GetItem(long id)
    {
        Item? item = await _repository.GetItemById(id);
        return new GetItemResponse()
        {
            Item = item
        };
    }
    public async Task<GetItemsResponse> GetItems()
    {
        var result = await _repository.GetItems();
        return new GetItemsResponse
        {
            Items = result
        };
    }
}
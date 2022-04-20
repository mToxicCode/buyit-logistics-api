using Dtos.Items;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items;

public interface IItemService
{
    public Task<long> CreateItem(CreateItemRequest request, CancellationToken cancellationToken);
    public Task DeleteItem(long request, CancellationToken cancellationToken);
    public Task ChangeItem(ChangeItemRequest request, CancellationToken cancellationToken);
    public Task<GetItemResponse?> GetItem(long id);
    public Task<GetItemsResponse> GetItems();
}
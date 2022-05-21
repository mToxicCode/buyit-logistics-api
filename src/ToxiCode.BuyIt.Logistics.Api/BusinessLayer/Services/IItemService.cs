using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;

public interface IItemService
{
    public Task DeleteItem(long request, CancellationToken cancellationToken);
    public Task ChangeItem(ChangeItemCommand command, CancellationToken cancellationToken);
    public Task<GetItemResponse?> GetItem(long id);
    public Task<GetItemsResponse> GetItems();
}
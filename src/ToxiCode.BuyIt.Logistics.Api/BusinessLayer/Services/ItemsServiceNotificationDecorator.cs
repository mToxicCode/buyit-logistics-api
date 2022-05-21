using System.Text.Json;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.Kafka;
using ToxiCode.BuyIt.Api.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using Action = ToxiCode.BuyIt.Api.Contracts.Action;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;

public class ItemsServiceNotificationDecorator : IItemService
{
    private readonly IItemService _target;
    private readonly IKafkaProducer _producer;

    public ItemsServiceNotificationDecorator(IItemService target, IKafkaProducer producer)
    {
        _target = target;
        _producer = producer;
    }
    

    public Task DeleteItem(long request, CancellationToken cancellationToken)
        => _target.DeleteItem(request, cancellationToken);

    public Task ChangeItem(ChangeItemCommand command, CancellationToken cancellationToken)
        => _target.ChangeItem(command, cancellationToken);

    public Task<GetItemResponse?> GetItem(long id)
        => _target.GetItem(id);

    public Task<GetItemsResponse> GetItems()
        => _target.GetItems();
}
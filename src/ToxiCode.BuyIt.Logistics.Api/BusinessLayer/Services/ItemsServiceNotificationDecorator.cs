using System.Text.Json;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.Kafka;
using ToxiCode.BuyIt.Api.Contracts;
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

    public async Task<long> CreateItem(CreateItemRequest request, CancellationToken cancellationToken)
    {
        var result = await _target.CreateItem(request, cancellationToken);
        var notification = new CreatedItemNotification()
        {
            Price = request.Item.Price,
            Name = request.Item.Name,
            Description = "Not provided",
            Id = result,
            OwnerId = Guid.NewGuid()
        };
        var wrapper = new KafkaMessage
        {
            CreatedItemNotification = notification,
            Action = Action.Created
        };
        var json = JsonSerializer.Serialize(wrapper);
        await _producer.SendMessageAsync(result.ToString(), json, cancellationToken);
        return result;
    }

    public Task DeleteItem(long request, CancellationToken cancellationToken)
        => _target.DeleteItem(request, cancellationToken);

    public Task ChangeItem(ChangeItemRequest request, CancellationToken cancellationToken)
        => _target.ChangeItem(request, cancellationToken);

    public Task<GetItemResponse?> GetItem(long id)
        => _target.GetItem(id);

    public Task<GetItemsResponse> GetItems()
        => _target.GetItems();
}
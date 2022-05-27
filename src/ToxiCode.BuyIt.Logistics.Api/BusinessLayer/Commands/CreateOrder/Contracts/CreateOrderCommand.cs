using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateOrder.Contracts;

public class CreateOrderCommand : IRequest<CreateOrderResponse>
{

    public IEnumerable<ItemPair> Items { get; set; } = null!;
    public long From { get; set; }
    public long To { get; set; }
    public string UserId { get; set; } = null!;
}

public class ItemPair{
    public long ItemId { get; set; }
    public int Count { get; set; }
}
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace Dtos;

public class OrderDto
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public long From { get; set; }
    public long To { get; set; }
    public IEnumerable<ItemInOrder> Items { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public string BuyerId { get; set; } = null!;
}

public class ItemInOrder
{
    public long ItemId { get; set; }
    public int Count { get; set; }
}
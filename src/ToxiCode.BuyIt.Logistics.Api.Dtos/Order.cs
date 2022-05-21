using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace Dtos;

public class Order
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Place From { get; set; } = null!;
    public Place To { get; set; } = null!;
    public long[]? Articles { get; set; } 
    public OrderStatus Status { get; set; } 
}
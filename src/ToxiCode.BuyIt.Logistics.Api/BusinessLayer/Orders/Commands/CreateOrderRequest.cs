using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;

public class CreateOrderRequest
{
    public DateTime CreatedDate { get; set; }
    public Place From { get; set; } = null!;
    public Place To { get; set; } = null!;
    public Dtos.Items.Item[] Items { get; set; } = null!;
}
using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;

public class CreateOrderRequest
{
    public DateTime CreatedDate { get; set; }
    public Place From { get; set; } = null!;
    public Place To { get; set; } = null!;
    public long[] Articles { get; set; } = null!;
}
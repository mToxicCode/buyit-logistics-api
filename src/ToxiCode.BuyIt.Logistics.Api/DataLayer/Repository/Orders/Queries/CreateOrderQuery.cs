namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;

public class CreateOrderQuery
{
    public DateTime CreationDate { get; set; }
    public long From { get; set; }
    public long To { get; set; } 
    public long[] Articles { get; set; } = null!;
    public string BuyerId { get; set; } = null!;
}
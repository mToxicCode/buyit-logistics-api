namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;

public class ChangeOrderRequest
{
    public long Id { get; set; } 
    public string Name { get; set; } = null!; 
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}
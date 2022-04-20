namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;

public class CreateItemRequest
{
    public string Name { get; set; } = null!; 
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}
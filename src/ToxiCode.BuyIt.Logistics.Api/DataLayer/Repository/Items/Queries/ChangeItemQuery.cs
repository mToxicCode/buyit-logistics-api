namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

public class ChangeItemQuery
{
    public long Id { get; set; }
    public string ItemName { get; set; } = null!;
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
}
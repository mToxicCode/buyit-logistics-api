namespace Dtos;

public class AddItemGrpcDto
{
    public Guid SellerId { get; set; }
    public string ItemName { get; set; } = null!; 
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public int Count { get; set; }
    public string ImgUrl { get; set; } = null!;
}
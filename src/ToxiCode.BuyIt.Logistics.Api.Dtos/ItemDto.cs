namespace Dtos;

public class ItemDto
{
    public long Id { get; set; }
    public Guid SellerId { get; set; }
    public string ItemName { get; set; } = null!; 
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public int AvailableCount { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ChangedAt { get; set; }
}
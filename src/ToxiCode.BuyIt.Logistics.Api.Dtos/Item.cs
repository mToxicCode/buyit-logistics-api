namespace Dtos;

public class Item
{
    public long Id { get; set; }
    public string Name { get; set; } = null!; 
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}
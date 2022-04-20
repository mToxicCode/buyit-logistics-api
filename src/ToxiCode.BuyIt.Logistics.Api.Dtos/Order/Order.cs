using Dtos.Items;

namespace Dtos.Order;

public class Order
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Place From { get; set; } = null!;
    public Place To { get; set; } = null!;
    public long[]? Articles { get; set; } 
}
using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;

public class ChangeItemRequest : IRequest
{
    public long Id { get; set; } 
    public string Name { get; set; } = null!; 
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
}
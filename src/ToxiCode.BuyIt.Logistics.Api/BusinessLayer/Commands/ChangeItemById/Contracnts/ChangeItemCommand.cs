using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;

public class ChangeItemCommand : IRequest
{
    public long Id { get; set; }
    public string ItemName { get; set; } = null!;
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
}
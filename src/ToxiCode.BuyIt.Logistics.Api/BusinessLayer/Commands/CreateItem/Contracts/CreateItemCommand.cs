using Dtos;
using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;

public class CreateItemCommand : IRequest<CreateItemResponse>
{
    public AddItemGrpcDto Item { get; set; } = null!;
}
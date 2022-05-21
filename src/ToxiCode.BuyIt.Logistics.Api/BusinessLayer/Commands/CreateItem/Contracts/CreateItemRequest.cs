using Dtos;
using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;

public class CreateItemRequest : IRequest<CreateItemResponse>
{
    public Item Item { get; set; } = null!;
}
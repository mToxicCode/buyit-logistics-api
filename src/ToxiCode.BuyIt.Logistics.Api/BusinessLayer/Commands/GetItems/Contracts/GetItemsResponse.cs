using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;

public class GetItemsResponse
{
    public IEnumerable<ItemDto>? Items { get; set; } = Array.Empty<ItemDto>();
}
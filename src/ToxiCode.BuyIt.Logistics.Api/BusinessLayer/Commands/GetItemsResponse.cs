using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;

public class GetItemsResponse
{
    public IEnumerable<ItemDto> Items { get; set; } = Array.Empty<ItemDto>();
}
using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;

public class GetItemsResponse
{
    public IEnumerable<Item>? Items { get; set; }
}
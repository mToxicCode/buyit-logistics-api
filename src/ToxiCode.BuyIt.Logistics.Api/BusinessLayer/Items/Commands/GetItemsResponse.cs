using Dtos.Items;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;

public class GetItemsResponse
{
    public IEnumerable<Item>? Items { get; set; }
}
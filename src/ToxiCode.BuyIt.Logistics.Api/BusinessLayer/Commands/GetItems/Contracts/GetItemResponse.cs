using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;

public class GetItemResponse
{
    public ItemDto? Item { get; set; }
}
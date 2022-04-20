using Dtos.Items;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.HttpControllers;

[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemServiceResolver _resolver;
    private readonly HttpCancellationTokenAccessor _cancellationToken;

    public ItemController(ItemsRepository repository, HttpCancellationTokenAccessor cancellationToken, ItemServiceResolver resolver)
    {
        _cancellationToken = cancellationToken;
        _resolver = resolver;
    }

    [HttpGet("api/item/all")]
    public async Task<ActionResult> GetItems() => Ok(await _resolver.GetItems());

    [HttpGet("api/item/{itemId:long}")]
    public async Task<ActionResult> GetItemById(long itemId) => Ok(await _resolver.GetItem(itemId));

    [HttpPost("api/item")]
    public async Task<ActionResult<long>> CreateItem([FromBody] CreateItemRequest request)
        => Ok(await _resolver.CreateItem(request, _cancellationToken.Token));

    [HttpDelete("api/item/{itemId:long}")]
    public async Task<ActionResult> DeleteItem(long itemId)
    {
        await _resolver.DeleteItem(itemId, _cancellationToken.Token);
        return Ok();
    }

    [HttpPut("api/item/")]
    public async Task<ActionResult> ChangeItem([FromBody] ChangeItemRequest request)
    {
        await _resolver.ChangeItem(request, _cancellationToken.Token);
        return Ok();
    }
}
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;

namespace ToxiCode.BuyIt.Logistics.Api.HttpControllers;

[ApiController]
public class ItemController : ControllerBase
{
    private readonly HttpCancellationTokenAccessor _cancellationToken;
    private readonly IMediator _mediator;

    public ItemController(HttpCancellationTokenAccessor cancellationToken, IMediator mediator)
    {
        _cancellationToken = cancellationToken;
        _mediator = mediator;
    }

    [HttpPost("api/itemsByIds/")]
    public async Task<ActionResult<ItemDto?>> GetItemByIds(long[] itemsIds)
    {
        var command = new GetItemsByIdsCommand
        {
            ItemIds = itemsIds
        };
        var result = await _mediator.Send(command);
        return Ok(result.Items);
    }

    [HttpGet("api/item/{itemId:long}")]
    public async Task<ActionResult<ItemDto?>> GetItemById(long itemId)
    {
        var command = new GetItemsByIdsCommand()
        {
            ItemIds = new[] {itemId}
        };
        var result = await _mediator.Send(command);
        return Ok(result.Items!.FirstOrDefault());
    }

    [HttpGet("api/items")]
    public async Task<ActionResult<IEnumerable<ItemDto?>>> GetItems()
    {
        var command = new GetItemsCommand();
        var result = await _mediator.Send(command);
        return Ok(result.Items);
    }

    [HttpDelete("api/item/{itemId:long}")]
    public async Task<ActionResult> DeleteItem(long itemId)
    {
        await _mediator.Send(itemId, _cancellationToken.Token);
        return Ok();
    }

    [HttpPut("api/item/")]
    public async Task<ActionResult> ChangeItem([FromBody] ChangeItemCommand command)
    {
        await _mediator.Send(command, _cancellationToken.Token);
        return Ok();
    }
}
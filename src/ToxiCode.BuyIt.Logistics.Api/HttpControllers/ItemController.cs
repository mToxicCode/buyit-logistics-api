using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;

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

    [HttpGet("api/item/{itemId:long}")]
    public async Task<ActionResult> GetItemById(long itemId)
    {
        return Ok(await _mediator.Send(itemId));
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
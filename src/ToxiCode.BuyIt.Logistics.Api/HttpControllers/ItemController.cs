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
    
    [HttpGet("api/item/all")]
    public async Task<ActionResult> GetItems(GetItemsRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    
    [HttpGet("api/item/{itemId:long}")]
    public async Task<ActionResult> GetItemById(long itemId)
    {
        return Ok(await _mediator.Send(itemId));
    }
    
    [HttpPost("api/item")]
    public async Task<ActionResult<long>> CreateItem([FromBody] CreateItemRequest request)
    {
        return Ok(await _mediator.Send(request, _cancellationToken.Token));
    }
    
    [HttpDelete("api/item/{itemId:long}")]
    public async Task<ActionResult> DeleteItem(long itemId)
    {
        await _mediator.Send(itemId, _cancellationToken.Token);
        return Ok();
    }

    [HttpPut("api/item/")]
    public async Task<ActionResult> ChangeItem([FromBody] ChangeItemRequest request)
    {
        await _mediator.Send(request, _cancellationToken.Token);
        return Ok();
    }
}
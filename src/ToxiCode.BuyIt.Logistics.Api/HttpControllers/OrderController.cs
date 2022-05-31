using Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateOrder.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;

namespace ToxiCode.BuyIt.Logistics.Api.HttpControllers;

[ApiController]
[Route("api")]
public class OrderController : ControllerBase
{
    private readonly HttpCancellationTokenAccessor _cancellationToken;
    private readonly IMediator _mediator;

    public OrderController(HttpCancellationTokenAccessor cancellationToken, IMediator mediator)
    {
        _cancellationToken = cancellationToken;
        _mediator = mediator;
    }

    [HttpGet("Orders")]
    public async Task<ActionResult<IEnumerable<OrderDto?>>> GetOrders()
    {
        var request = new GetOrdersCommand();
        var result = await _mediator.Send(request, _cancellationToken.Token);
        return Ok(result);
    }

    // [HttpGet("api/Order/{orderId:long}")]
    // public async Task<ActionResult> GetOrderByBuyerId(long orderId) => Ok(await _resolver.GetOrder(orderId));

    [HttpPost("Order/Create")]
    public async Task<ActionResult<long>> CreateOrder([FromBody] CreateOrderCommand request)
        => Ok(await _mediator.Send(request, _cancellationToken.Token));
    //
    // [HttpDelete("api/Order/{orderId:long}")]
    // public async Task<ActionResult> DeleteOrder(long orderId)
    // {
    //     await _resolver.DeleteOrder(orderId, _cancellationToken.Token);
    //     return Ok();
    // }
    //
    // [HttpPut("api/Order/")]
    // public async Task<ActionResult> ChangeOrder([FromBody] ChangeOrderRequest request)
    // {
    //     await _resolver.ChangeOrder(request, _cancellationToken.Token);
    //     return Ok();
    // }
}
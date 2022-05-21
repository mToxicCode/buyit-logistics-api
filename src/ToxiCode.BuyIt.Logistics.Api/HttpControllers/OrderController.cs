using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeOrderById.Contracts;

namespace ToxiCode.BuyIt.Logistics.Api.HttpControllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly HttpCancellationTokenAccessor _cancellationToken;

    public OrderController(HttpCancellationTokenAccessor cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    // [HttpGet("api/Order/all")]
    // public async Task<ActionResult> GetOrders() => Ok(await _resolver.GetOrders());
    //
    // [HttpGet("api/Order/{orderId:long}")]
    // public async Task<ActionResult> GetOrderById(long orderId) => Ok(await _resolver.GetOrder(orderId));
    //
    // [HttpPost("api/Order")]
    // public async Task<ActionResult<long>> CreateOrder([FromBody] CreateOrderRequest request)
    //     => Ok(await _resolver.CreateOrder(request, _cancellationToken.Token));
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
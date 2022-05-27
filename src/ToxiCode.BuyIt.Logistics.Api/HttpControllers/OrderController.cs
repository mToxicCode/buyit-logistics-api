using Dtos;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeOrderById.Contracts;
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

    [HttpGet("orders")]
    public async Task<ActionResult<IEnumerable<OrderDto?>>> GetOrders()
    {
        var request = new GetOrdersCommand();
        var result = await _mediator.Send(request);
        return Ok(result.Orders);
    }
    
    [HttpGet("orderById/{orderId:long}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(long orderId)
    {
        var command = new GetOrderByIdCommand
        {
            OrderId = orderId
        };
        var result = await _mediator.Send(command);
        return Ok(result.Orders);
    }

    [HttpGet("order/{buyerId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderByBuyerId(string buyerId)
    {
        var command = new GetOrdersByBuyerIdCommand
        {
            BuyerId = buyerId,
        };
        
        var response = await _mediator.Send(command); 
        return Ok(response.Orders);
    }

    [HttpPost("order/create")]
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
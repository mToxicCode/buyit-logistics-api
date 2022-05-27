using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateOrder.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.SetOrderStatus.Contracts;
using ToxiCode.BuyIt.Logistics.Api.Grpc;
using GetOrdersByBuyerIdResponse = ToxiCode.BuyIt.Logistics.Api.Grpc.GetOrdersByBuyerIdResponse;

namespace ToxiCode.BuyIt.Logistics.Api.GrpcControllers;

public class OrdersGrpcController : OrdersService.OrdersServiceBase
{
    private readonly IMediator _mediator;

    public OrdersGrpcController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<AddOrderResponse> AddOrder(AddOrderRequest request, ServerCallContext context)
    {
        var command = new CreateOrderCommand
        {
            From = request.FromAddressId,
            To = request.ToAddressId,
            UserId = request.UserId,
            Items = request.Items.Select(x => new ItemPair
            {
                Count = x.Count,
                ItemId = x.ItemId
            })
        };
        var response = await _mediator.Send(command);
        return new AddOrderResponse
        {
            OrderId = response.OrderId,
            ResultMessage = "Success"
        };
    }

    public override async Task<OrderPaidResponse> OrderPaid(OrderPaidRequest request, ServerCallContext context)
    {
        var command = new SetOrderStatusCommand
        {
            Status = OrderStatus.Processing,
            OrderId = request.OrderId
        };
        await _mediator.Send(command);
        return new OrderPaidResponse
        {
            ResultMessage = "Success"
        };
    }

    public override async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, ServerCallContext context)
    {
        var command = new SetOrderStatusCommand
        {
            Status = OrderStatus.Cancelled,
            OrderId = request.OrderId
        };
        await _mediator.Send(command);
        return new CancelOrderResponse
        {
            ResultMessage = "Success"
        };
    }

    public override async Task<GetOrdersByBuyerIdResponse> GetOrdersByBuyerId(GetOrdersByBuyerIdRequest request, ServerCallContext context)
    {
        var command = new GetOrdersByBuyerIdCommand
        {
            BuyerId = request.BuyerId
        };
        var orders = await _mediator.Send(command);
        if (orders.Orders != null)
            return new GetOrdersByBuyerIdResponse
            {
                Orders =
                {
                    orders.Orders.Select(x => new Order
                    {
                        OrderId = x.Id,
                        CreatedDate = DateTime.SpecifyKind(x.CreationDate, DateTimeKind.Utc).ToTimestamp(),
                        OrderStatus = x.Status,
                        FromAddressId = x.From,
                        ToAddressId = x.To,
                        Items =
                        {
                            x.Items.Select(y => new ItemAmountPair
                            {
                                ItemId = y.ItemId,
                                Count = y.Count,
                            })
                        },
                        BuyerId = x.BuyerId
                    })
                }
            };
        return new GetOrdersByBuyerIdResponse();
    }
}
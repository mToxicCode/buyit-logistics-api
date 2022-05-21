using Grpc.Core;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.GrpcControllers;

public class OrdersGrpcController : OrdersService.OrdersServiceBase
{
    public override Task<AddOrderResponse> AddOrder(AddOrderRequest request, ServerCallContext context) => base.AddOrder(request, context);
    public override Task<GetOrdersResponse> GetOrders(GetOrdersRequest request, ServerCallContext context) => base.GetOrders(request, context);
}
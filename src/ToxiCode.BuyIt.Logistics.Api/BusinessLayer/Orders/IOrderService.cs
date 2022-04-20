using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders;

public interface IOrderService
{
    public Task<long> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
    public Task DeleteOrder(long request, CancellationToken cancellationToken);
    public Task ChangeOrder(ChangeOrderRequest request, CancellationToken cancellationToken);
    public Task<GetOrderResponse?> GetOrder(long id);
    public Task<GetOrdersResponse> GetOrders();
}
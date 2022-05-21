using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeOrderById.Contracts;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;

public interface IOrderService
{
    public Task<long> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
    public Task DeleteOrder(long request, CancellationToken cancellationToken);
    public Task ChangeOrder(ChangeOrderRequest request, CancellationToken cancellationToken);
    public Task<GetOrderResponse?> GetOrder(long id);
    public Task<GetOrdersResponse> GetOrders();
}
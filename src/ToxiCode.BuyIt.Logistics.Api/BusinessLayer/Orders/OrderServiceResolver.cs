using Dtos.Order;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders;

public class OrderServiceResolver : IOrderService
{
    private readonly OrdersRepository _repository;

    public OrderServiceResolver(OrdersRepository repository) => _repository = repository;

    public Task<long> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken) 
        => _repository.CreateOrder(request, cancellationToken);

    public Task DeleteOrder(long id, CancellationToken cancellationToken)
        => _repository.DeleteOrderById(id, cancellationToken);

    public Task ChangeOrder(ChangeOrderRequest request, CancellationToken cancellationToken)
        => _repository.ChangeOrder(request, cancellationToken);

    public async Task<GetOrderResponse?> GetOrder(long id)
    {
        Order? order = await _repository.GetOrderById(id);
        return new GetOrderResponse()
        {
            Order = order
        };
    }
    public async Task<GetOrdersResponse> GetOrders()
    {
        var result = await _repository.GetOrders();
        return new GetOrdersResponse
        {
            Orders = result
        };
    }
}
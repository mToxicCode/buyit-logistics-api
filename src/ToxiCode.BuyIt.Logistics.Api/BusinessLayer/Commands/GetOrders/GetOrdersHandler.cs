using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders;

public class GetOrdersHandler : IRequestHandler<GetOrdersCommand, GetOrdersResponse>
{
    private readonly OrdersRepository _ordersRepository;
    private ItemsRepository _itemsRepository;

    public GetOrdersHandler(OrdersRepository ordersRepository, ArticlesRepository articlesRepository, ItemsRepository itemsRepository)
    {
        _ordersRepository = ordersRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task<GetOrdersResponse> Handle(GetOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetOrders();
        foreach (var order in orders)
        {
            order.Items = await _itemsRepository.GetItemsByOrderId(order.Id);
        }
        return new GetOrdersResponse
        {
            Orders = orders.ToArray()
        };
    }
}
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;
using ToxiCode.BuyIt.Logistics.Api.Grpc;
using GetOrdersByBuyerIdResponse = ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts.GetOrdersByBuyerIdResponse;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders;

public class GetOrdersByBuyerIdHandler : IRequestHandler<GetOrdersByBuyerIdCommand, GetOrdersByBuyerIdResponse>
{
    private readonly OrdersRepository _ordersRepository;
    private readonly ArticlesRepository _articlesRepository;
    private readonly ItemsRepository _itemsRepository;

    public GetOrdersByBuyerIdHandler(OrdersRepository ordersRepository, ArticlesRepository articlesRepository, ItemsRepository itemsRepository)
    {
        _ordersRepository = ordersRepository;
        _articlesRepository = articlesRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task<GetOrdersByBuyerIdResponse> Handle(GetOrdersByBuyerIdCommand request, CancellationToken cancellationToken)
    {
        var query = new GetOrdersByBuyerIdQuery
        {
            BuyerId = request.BuyerId
        };
        var orders = await _ordersRepository.GetOrdersByBuyerId(query);
        foreach (var order in orders)
        {
            // order.Items = await _itemsRepository.GetItemsByOrderId(order.Id);
        }

        return new GetOrdersByBuyerIdResponse
        {
            Orders = orders
        };
    }
}
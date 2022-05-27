using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdCommand, GetOrderByIdResponse>
{
    private readonly OrdersRepository _ordersRepository;
    private readonly ItemsRepository _itemsRepository;

    public GetOrderByIdHandler(OrdersRepository ordersRepository, ItemsRepository itemsRepository)
    {
        _ordersRepository = ordersRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery
        {
            OrderId = request.OrderId
        };
        var order = await _ordersRepository.GetOrderById(query);

        if (order == null)
            return new GetOrderByIdResponse();

        order.Items = await _itemsRepository.GetItemsByOrderId(order.Id);


        return new GetOrderByIdResponse
        {
            Orders = new OrderDto
            {
                From = order!.From,
                To = order.To,
                Items = order.Items,
                Id = order.Id,
                Status = order.Status,
                BuyerId = order.BuyerId,
                CreationDate = order.CreationDate,
            }
        };
    }
}
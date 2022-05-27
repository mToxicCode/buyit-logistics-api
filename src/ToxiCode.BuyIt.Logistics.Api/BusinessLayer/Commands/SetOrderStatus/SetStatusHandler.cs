using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.SetOrderStatus.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.SetOrderStatus;

public class SetStatusHandler : AsyncRequestHandler<SetOrderStatusCommand>
{
    private OrdersRepository _ordersRepository;
    private readonly OrdersServiceNotificator _notificator;

    public SetStatusHandler(OrdersRepository ordersRepository, OrdersServiceNotificator notificator)
    {
        _ordersRepository = ordersRepository;
        _notificator = notificator;
    }

    protected override async Task Handle(SetOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var query = new SetOrderStatusQuery
        {
            Status = request.Status,
            OrderId = request.OrderId
        };
        await _ordersRepository.SetOrderStatus(query);
        await _notificator.NotifyStatusChanged(query.OrderId, query.Status, cancellationToken);
    }
}
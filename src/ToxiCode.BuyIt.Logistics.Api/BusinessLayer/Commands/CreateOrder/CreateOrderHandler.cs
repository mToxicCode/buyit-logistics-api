using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateOrder.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly OrdersRepository _ordersRepository;
    private readonly ArticlesRepository _articlesRepository;
    private readonly OrdersServiceNotificator _notificator;

    public CreateOrderHandler(OrdersRepository ordersRepository, ArticlesRepository articlesRepository, OrdersServiceNotificator notificator)
    {
        _ordersRepository = ordersRepository;
        _articlesRepository = articlesRepository;
        _notificator = notificator;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var articles = new List<long>();
        foreach (var item in request.Items)
        {
            var articlesQuery = new CreateArticleByItemIdQuery
            {
                Count = item.Count,
                ItemId = item.ItemId
            };
            var itemArticles = await _articlesRepository.GetArticlesByItemId(articlesQuery);
            articles.AddRange(itemArticles.Select(article => article.Id));
        }
      
        var query = new CreateOrderQuery
        {
            CreationDate = DateTime.Now,
            From = request.From,
            To = request.To,
            Articles = articles.ToArray(),
            BuyerId = request.UserId
        };
        var orderId = await _ordersRepository.CreateOrder(query, cancellationToken);
        var result = new CreateOrderResponse
        {
            OrderId = orderId
        };
        await _notificator.NotifyStatusChanged(orderId, OrderStatus.Created, cancellationToken);
        return result;
    }
}
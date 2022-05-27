using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetOrders;

public class GetOrdersHandler : IRequestHandler<GetOrdersCommand, GetOrdersResponse>
{
    private readonly OrdersRepository _ordersRepository;
    private ArticlesRepository _articlesRepository;

    public GetOrdersHandler(OrdersRepository ordersRepository, ArticlesRepository articlesRepository)
    {
        _ordersRepository = ordersRepository;
        _articlesRepository = articlesRepository;
    }

    public async Task<GetOrdersResponse> Handle(GetOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetOrders();
        foreach (var order in orders)
        {
            var articlesQuery = new GetArticlesByOrderIdQuery
            {
                OrderId = order.Id
            };
            IEnumerable<long> articlesInOrder = await _articlesRepository.GetArticlesByOrderId(articlesQuery);
            foreach (var article in articlesInOrder)
            {
                var articleQuery = new GetItemIdByArticleQuery
                {
                    ArticleId = article
                };
                var itemId = await _articlesRepository.GetItemIdByArticle(articleQuery);
            }
        }

        return new GetOrdersResponse
        {
            Orders = orders
        };
    }
}
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateArticleByItemId.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem;

public class CreateItemHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly ItemsRepository _itemsRepository;
    private readonly ArticlesRepository _articlesRepository;

    public CreateItemHandler(ItemsRepository itemsRepository, ArticlesRepository articlesRepository)
    {
        _itemsRepository = itemsRepository;
        _articlesRepository = articlesRepository;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var query = new CreateItemQuery
        {
            Item = request.Item
        };
        var id = await _itemsRepository.CreateItem(query, cancellationToken);

        var articleQuery = new CreateArticleByItemIdQuery
        {
            ItemId = id,
            Count = request.Item.Count
        };
        
        await _articlesRepository.CreateArticleByItemId(articleQuery, cancellationToken);
        
        return new CreateItemResponse()
        {
            Id = id
        };
    }
}
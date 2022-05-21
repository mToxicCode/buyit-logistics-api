using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateArticleByItemId.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateArticleByItemId;

public class CreateArticleByItemIdHandler : AsyncRequestHandler<CreateArticleByItemIdCommand> 
{
    private readonly ArticlesRepository _articlesRepository;

    public CreateArticleByItemIdHandler(ArticlesRepository articlesRepository) 
        => _articlesRepository = articlesRepository;

    protected override async Task Handle(CreateArticleByItemIdCommand request, CancellationToken cancellationToken)
    {
        var query = new CreateArticleByItemIdQuery()
        {
            ItemId = request.ItemId,
            Count = request.Count
        };
        await _articlesRepository.CreateArticleByItemId(query, cancellationToken);
    }
}
using MediatR;
using MediatR.Wrappers;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.AddArticlesByItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.AddArticlesByItem;

public class AddArticlesByItemHandler : IRequestHandler<CreateArticleRequest, CreateArticleRespone> 
{
    private readonly ArticlesRepository _articlesRepository;

    public AddArticlesByItemHandler(ArticlesRepository articlesRepository) 
        => _articlesRepository = articlesRepository;

    public async Task<CreateArticleRespone> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        return await _articlesRepository.CreateArticles(request, cancellationToken);
    }
}
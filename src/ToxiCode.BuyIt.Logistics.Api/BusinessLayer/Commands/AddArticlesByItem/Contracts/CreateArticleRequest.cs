using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.AddArticlesByItem.Contracts;

public class CreateArticleRequest :  IRequest<CreateArticleRespone>
{
    public int Count;
    public long ItemId;
}
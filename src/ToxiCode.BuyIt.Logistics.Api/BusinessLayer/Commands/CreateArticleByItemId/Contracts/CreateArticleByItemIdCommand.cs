using MediatR;

namespace ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateArticleByItemId.Contracts;

public class CreateArticleByItemIdCommand : IRequest
{
    public int Count;
    public long ItemId;
}
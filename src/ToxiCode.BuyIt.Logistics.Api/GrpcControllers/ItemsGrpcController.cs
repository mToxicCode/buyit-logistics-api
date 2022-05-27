using Dtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeItemById.Contracnts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateArticleByItemId.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.CreateItem.Contracts;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.GetItems.Contracts;
using GRPC = ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.GrpcControllers;

public class ItemsGrpcController : GRPC.ItemsService.ItemsServiceBase
{
    private readonly IMediator _mediator;

    public ItemsGrpcController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GRPC.AddItemResponse> AddItem(GRPC.AddItemRequest request, ServerCallContext context)
    {
        CreateItemCommand command = new()
        {
            Item = new AddItemGrpcDto
            {
                ItemName = request.AddItem.ItemName,
                SellerId = Guid.Parse(request.AddItem.SellerId),
                Weight = request.AddItem.Weight,
                Height = request.AddItem.Height,
                Length = request.AddItem.Length,
                Width = request.AddItem.Width,
                Count = request.AddItem.Count,
                ImgUrl = request.AddItem.ImgUrl
            }
        };

        var response = await _mediator.Send(command);

        var result = new GRPC.AddItemResponse
        {
            AddItemResult = new GRPC.ItemResult
            {
                ItemId = response.Id,
                ResultMessage = "Success"
            }
        };
        return result;
    }

    public override async Task<GRPC.GetItemsByIdsResponse> GetItemsByIds(GRPC.GetItemsByIdsRequest request, ServerCallContext context)
    {
        GetItemsByIdsCommand command = new()
        {
            ItemIds = request.ItemsIds
        };
        var response = await _mediator.Send(command);

        var result = new GRPC.GetItemsByIdsResponse
        {
            Items =
            {
                response.Items!.Select(x =>
                    new GRPC.Item
                    {
                        ItemId = x.Id,
                        ItemName = x.ItemName,
                        SellerId = x.SellerId.ToString(),
                        Weight = x.Weight,
                        Height = x.Height,
                        Length = x.Length,
                        Width = x.Width,
                        AvailableCount = x.AvailableCount,
                        CreationDate = DateTime.SpecifyKind(x.CreationDate, DateTimeKind.Utc).ToTimestamp(),
                        ChangedAt = DateTime.SpecifyKind(x.ChangedAt, DateTimeKind.Utc).ToTimestamp()
                    }
                )
            }
        };
        return result;
    }

    public override async Task<GRPC.ChangeItemResponse> ChangeItem(GRPC.ChangeItemRequest request, ServerCallContext context)
    {
        var command = new ChangeItemCommand
        {
            Id = request.ItemId,
            ItemName = request.ItemName,
            Weight = request.Weight,
            Height = request.Height,
            Length = request.Length,
            Width = request.Width
        };

        await _mediator.Send(command, context.CancellationToken);

        return new GRPC.ChangeItemResponse
        {
            ItemResult = new GRPC.ItemChangeResult
            {
                ItemId = request.ItemId,
                ItemChanged = true,
                ResultMessage = "Success",
            }
        };
    }

    public override async Task<GRPC.AddArticlesResponse> AddArticles(GRPC.AddArticlesRequest request, ServerCallContext context)
    {
        var command = new CreateArticleByItemIdCommand
        {
            ItemId = request.ItemId,
            Count = request.Count
        };

        await _mediator.Send(command, context.CancellationToken);

        return new GRPC.AddArticlesResponse
        {
            ResultMessage = "Success"
        };
    }
}
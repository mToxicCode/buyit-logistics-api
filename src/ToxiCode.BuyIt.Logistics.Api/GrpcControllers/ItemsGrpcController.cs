using Grpc.Core;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.GrpcControllers;

public class ItemsGrpcController : ItemsService.ItemsServiceBase
{
    public override Task<AddItemsResponse> AddItems(AddItemsRequest request, ServerCallContext context) 
        => base.AddItems(request, context);

    public override Task<GetItemsResponse> GetItems(GetItemsRequest request, ServerCallContext context) => base.GetItems(request, context);

    public override Task<ChangeItemsResponse> ChangeItems(ChangeItemsRequest request, ServerCallContext context) => base.ChangeItems(request, context);
}
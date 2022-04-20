using Grpc.Core;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.GrpcControllers;

public class CommonBuyItLogisticsService : Grpc.CommonBuyItLogisticsService.CommonBuyItLogisticsServiceBase
{
    public override async Task<AddItemsResponse> AddItems(AddItemsRequest request, ServerCallContext context)
    {
        await Task.Delay(1);
        return new AddItemsResponse
        {
            Items =
            {
                new[]
                {
                    new ItemModel()
                    {
                        Id = 1,
                        Name = "Тест1"
                    },
                    new ItemModel()
                    {
                        Id = 2,
                        Name = "Тест2"
                    },
                    new ItemModel()
                    {
                        Id = 0,
                        Name = "Тест3"
                    }
                }
            }
        };
    }
}
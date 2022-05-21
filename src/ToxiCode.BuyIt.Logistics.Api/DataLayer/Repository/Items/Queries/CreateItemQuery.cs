using Dtos;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

public class CreateItemQuery
{
    public AddItemGrpcDto Item { get; set; } = null!;
}
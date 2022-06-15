namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;

public class GetItemsByIdsQuery
{
    public IEnumerable<long> Ids { get; set; } = null!;
}
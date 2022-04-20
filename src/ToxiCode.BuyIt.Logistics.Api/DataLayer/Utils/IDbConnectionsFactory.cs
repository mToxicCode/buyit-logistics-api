namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

public interface IDbConnectionFactory
{
    DatabaseWrapper CreateDatabase(CancellationToken? cancellationToken = default);
}
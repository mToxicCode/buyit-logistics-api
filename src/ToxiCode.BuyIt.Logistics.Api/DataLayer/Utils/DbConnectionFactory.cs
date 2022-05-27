using Npgsql;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly HttpCancellationTokenAccessor _cancellationTokenAccessor;
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(
        HttpCancellationTokenAccessor cancellationTokenAccessor,
        IConfiguration configuration)
    {
        _cancellationTokenAccessor = cancellationTokenAccessor;
        _configuration = configuration;
    }

    public DatabaseWrapper CreateDatabase(CancellationToken? cancellationToken = default)
        => new(
            new NpgsqlConnection(Environment.GetEnvironmentVariable("DATABASE_STRING")),
            cancellationToken ?? _cancellationTokenAccessor.Token);
}
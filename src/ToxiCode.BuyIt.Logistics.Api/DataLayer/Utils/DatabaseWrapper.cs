using System.Data;
using System.Data.Common;
using Dapper;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

/// <summary>
/// Wrapper for database connection
/// </summary>
public class DatabaseWrapper : IAsyncDisposable
{
    private const int DatabaseDefaultTimeout = 17;
    private readonly CancellationToken _cancellationToken;

    public DatabaseWrapper(DbConnection connection, CancellationToken cancellationToken)
    {
        Connection = connection;
        _cancellationToken = cancellationToken;
    }

    /// <summary>
    /// Database connection
    /// </summary>
    public DbConnection Connection { get; }

    /// <summary>
    /// Method that creates command to execute 
    /// </summary>
    /// <param name="commandText">Sql query</param>
    /// <param name="parameters">parameters</param>
    /// <param name="commandType">commandType</param>
    /// <param name="commandTimeout">commandTimeout</param>
    /// <param name="transaction">transaction</param>
    /// <returns>Command definition </returns>
    public CommandDefinition CreateCommand(
        string commandText,
        object? parameters = null,
        CommandType? commandType = null,
        int? commandTimeout = null,
        IDbTransaction? transaction = null)
        => new(
            commandText,
            parameters,
            commandTimeout: commandTimeout ?? DatabaseDefaultTimeout,
            commandType: commandType ?? CommandType.Text,
            transaction: transaction,
            cancellationToken: _cancellationToken);

    public ValueTask DisposeAsync()
        => Connection.DisposeAsync();
}
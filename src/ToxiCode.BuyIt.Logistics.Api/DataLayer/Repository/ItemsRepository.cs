using Dapper;
using Dtos.Items;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Items.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;


namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

public class ItemsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ItemsRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<Item>> GetItems()
    {
        const string getItemsQuery = $@"SELECT id, name, price, weight FROM {SqlConstants.Items}";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<Item>(db.CreateCommand(getItemsQuery));
    }

    public async Task<Item?> GetItemById(long itemId)
    {
        const string getItemByIdQuery = $@"SELECT id, name, price, weight FROM {SqlConstants.Items} WHERE id = @Id";
        await using var db = _connectionFactory.CreateDatabase();
        var requestItem  = await db.Connection.QueryFirstOrDefaultAsync<Item>(db.CreateCommand(getItemByIdQuery, new
        {
            Id = itemId
        }));
        return requestItem;
    }

    public async Task<long> CreateItem(CreateItemRequest request, CancellationToken cancellationToken)
    {
        const string insertItemQuery =
            $@"INSERT INTO {SqlConstants.Items} 
                    (name, price, weight)
            VALUES (@Name, @Price, @Weight) returning id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        var id = await db.Connection.QueryFirstOrDefaultAsync<long>(insertItemQuery, new
        {
            request.Name,
            request.Price,
            request.Weight
        });
        return id;
    }

    public async Task DeleteItemById(long itemId, CancellationToken cancellationToken)
    {
        const string DeleteItemQuery = $@"DELETE FROM {SqlConstants.Items} WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(DeleteItemQuery, new
        {
            Id = itemId
        });
    }

    public async Task ChangeItem(ChangeItemRequest request, CancellationToken cancellationToken)
    {
        const string ChangeItemQuery = $@"UPDATE {SqlConstants.Items}  
                                            SET name = @Name, price = @Price, weight = @Weight WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(ChangeItemQuery, new
        {
            request.Id,
            request.Name,
            request.Price,
            request.Weight
        });
    }
}
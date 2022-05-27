using Dapper;
using Dtos;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;

public class ItemsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ItemsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ItemDto>> GetItemsByIds(GetItemsByIdsQuery ids)
    {
        const string getItemsQuery = $@"  SELECT 
									 it.id, 
									 seller_id SellerId,
                                     item_name ItemName, 
                                     weight, 
                                     height, 
                                     length, 
                                     width, 
                                     it.creation_date CreationDate, 
                                     changed_at ChangedAt,
                                     (SELECT count(a.id) AvailableCount 
                                     FROM public.articles a
                                       LEFT JOIN public.articles_in_order aio ON aio.article_id = a.id
                                       LEFT JOIN public.orders o ON aio.order_id = o.id
                                        WHERE a.item_id = it.id AND o.id IS NULL OR o.state = 'Cancelled'::State)
                                        FROM items it
                                        WHERE it.id = any(@Ids)";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<ItemDto>(db.CreateCommand(getItemsQuery, new
        {
            Ids = ids
        }));
    }

    public async Task<IEnumerable<ItemDto>> GetItems()
    {
        const string getItemsQuery = $@"  SELECT 
									 it.id, 
									 seller_id SellerId,
                                     item_name ItemName, 
                                     weight, 
                                     height, 
                                     length, 
                                     width, 
                                     it.creation_date CreationDate, 
                                     changed_at ChangedAt,
                                     (SELECT count(a.id) AvailableCount 
                                     FROM public.articles a
                                       LEFT JOIN public.articles_in_order aio ON aio.article_id = a.id
                                       LEFT JOIN public.orders o ON aio.order_id = o.id
                                        WHERE a.item_id = it.id AND o.id IS NULL OR o.state = 'Cancelled'::State)
                                        FROM items it";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<ItemDto>(db.CreateCommand(getItemsQuery));
    }

    public async Task<long> CreateItem(CreateItemQuery request, CancellationToken cancellationToken)
    {
        var insertItemQuery = $@"INSERT INTO {SqlConstants.Items} 
                    (item_name, seller_id, weight, height, length, width";

        if (!string.IsNullOrEmpty(request.Item.ImgUrl))
            insertItemQuery += ", img_url) VALUES (@ItemName, @SellerId, @Weight, @Height, @Length, @Width, @ImgUrl) returning id";
        else
            insertItemQuery += ") VALUES (@ItemName, @SellerId, @Weight, @Height, @Length, @Width) returning id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        var id = await db.Connection.QueryFirstOrDefaultAsync<long>(insertItemQuery, new
        {
            request.Item.ItemName,
            request.Item.SellerId,
            request.Item.Weight,
            request.Item.Height,
            request.Item.Length,
            request.Item.Width,
            request.Item.ImgUrl
        });
        return id;
    }

    public async Task<IEnumerable<ItemInOrder>> GetItemsByOrderId(long orderId)
    {
        const string query = @"select i.id item_id, i.item_name ItemName from articles a 
    inner join articles_in_order aio on aio.article_id = a.id 
    inner join orders o on aio.order_id = o.id 
    inner join items i on a.item_id = i.id 
    where o.id = @OrderId";

        await using var db = _connectionFactory.CreateDatabase();

        var result = (await db.Connection.QueryAsync<(long, string)>(query, new
        {
            OrderId = orderId
        })).ToArray();

        var intersected = result.Distinct();

        var items = intersected
            .Select(item =>
                new ItemInOrder
                {
                    ItemId = item.Item1,
                    ItemName = item.Item2,
                    Count = result.Count(x => x == item)
                }).ToList();

        return items;
    }

    public async Task DeleteItemById(long itemId, CancellationToken cancellationToken)
    {
        const string deleteItemQuery = $@"DELETE FROM {SqlConstants.Items} WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(deleteItemQuery, new
        {
            Id = itemId
        });
    }

    public async Task ChangeItem(ChangeItemQuery command, CancellationToken cancellationToken)
    {
        const string ChangeItemQuery = $@"UPDATE {SqlConstants.Items} 
                                            SET 
                                            item_name = @ItemName,
                                            weight = @Weight,
                                            height = @Height,
                                            length = @Length,
                                            width = @Width,
                                            changed_at = @ChangedAt
                                            WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(ChangeItemQuery, new
        {
            command.Id,
            command.ItemName,
            command.Weight,
            command.Height,
            command.Length,
            command.Width,
            ChangedAt = DateTime.Now
        });
    }
}
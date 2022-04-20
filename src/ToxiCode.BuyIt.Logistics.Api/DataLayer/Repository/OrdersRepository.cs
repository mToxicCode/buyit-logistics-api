using Dapper;
using Dtos.Order;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Orders.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

public class OrdersRepository

{
    private readonly IDbConnectionFactory _connectionFactory;

    public OrdersRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<Order>> GetOrders()
    {
        const string getOrdersQuery = $@"SELECT id, name, price, weight FROM {SqlConstants.Orders}";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<Order>(db.CreateCommand(getOrdersQuery));
    }

    public async Task<Order?> GetOrderById(long OrderId)
    {
        const string getOrderByIdQuery = $@"SELECT id, name, price, weight FROM {SqlConstants.Orders} WHERE id = @Id";
        await using var db = _connectionFactory.CreateDatabase();
        Order requestOrder = await db.Connection.QueryFirstOrDefaultAsync<Order>(db.CreateCommand(getOrderByIdQuery, new
        {
            Id = OrderId
        }));
        return requestOrder ;
    }

    public async Task<long> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        const string insertOrderQuery =
            $@"INSERT INTO {SqlConstants.Orders} 
                    (date_time, from, to)
            VALUES (@DateTime, @From, @To) returning id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        var articleId = await db.Connection.QueryFirstOrDefaultAsync<long>(insertOrderQuery, new
        {
            request.From,
            request.To,
        });

        const string insertItemsInOrderQuery =
            $@"INSERT INTO {SqlConstants.Articles} 
                    (article_id, item_id)
            VALUES (@ArticleId, @ItemId) returning id";

        await db.Connection.ExecuteAsync(insertItemsInOrderQuery, request.Items.Select(x => new
        {
            //Тут ты хотел написать логику и добавить таблицу (Артикл, айди предмета) и добавлять сюда артикл, и связывать артикл с ордер айд
            //А еще изменить, что бы в ордере не была таблица айтемс
        }));

        return articleId;
    }

    public async Task DeleteOrderById(long OrderId, CancellationToken cancellationToken)
    {
        const string DeleteOrderQuery = $@"DELETE FROM {SqlConstants.Orders} WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(DeleteOrderQuery, new
        {
            Id = OrderId
        });
    }

    public async Task ChangeOrder(ChangeOrderRequest request, CancellationToken cancellationToken)
    {
        const string ChangeOrderQuery = $@"UPDATE {SqlConstants.Orders}  
                                            SET name = @Name, price = @Price, weight = @Weight WHERE id = @Id ";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        await db.Connection.ExecuteAsync(ChangeOrderQuery, new
        {
            request.Id,
            request.Name,
            request.Price,
            request.Weight
        });
    }
}
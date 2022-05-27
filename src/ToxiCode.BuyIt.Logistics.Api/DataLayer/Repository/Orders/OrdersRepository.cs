using Dapper;
using Dtos;
using MediatR;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Commands.ChangeOrderById.Contracts;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;
using ToxiCode.BuyIt.Logistics.Api.Grpc;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;

public class OrdersRepository

{
    private readonly IDbConnectionFactory _connectionFactory;

    public OrdersRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        const string getOrdersQuery = $@"SELECT id AS Id, 
                                        creation_date AS CreationDate, 
                                        ""from"",
                                        ""to"",
                                        ""state"" AS Status,
                                        buyer_id AS BuyerId 
                                        FROM {SqlConstants.Orders}";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<OrderDto>(db.CreateCommand(getOrdersQuery));
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByBuyerId(GetOrdersByBuyerIdQuery request)
    {
        const string getOrdersQuery = $@"SELECT id AS Id, 
                                        creation_date AS CreationDate, 
                                        ""from"",
                                        ""to"",
                                        ""state"" AS Status,
                                        buyer_id AS BuyerId 
                                        FROM {SqlConstants.Orders}
                                        WHERE buyer_id = @BuyerId";
        await using var db = _connectionFactory.CreateDatabase();
        var orders = await db.Connection.QueryAsync<OrderDto>(db.CreateCommand(getOrdersQuery, new
        {
            request.BuyerId
        }));
        return orders;
    }

    public async Task<OrderDto?> GetOrderById(long OrderId)
    {
        const string getOrderByIdQuery = $@"SELECT id, name, price, weight FROM {SqlConstants.Orders} WHERE id = @Id";
        await using var db = _connectionFactory.CreateDatabase();
        OrderDto requestOrder = await db.Connection.QueryFirstOrDefaultAsync<OrderDto>(db.CreateCommand(getOrderByIdQuery, new
        {
            Id = OrderId
        }));
        return requestOrder;
    }

    public async Task<long> CreateOrder(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        // Создание заказа

        const string insertOrderQuery =
            $@"INSERT INTO {SqlConstants.Orders} 
                    (creation_date, ""from"", ""to"", state, buyer_id)
            VALUES (@CreationDate, @From, @To, @Status::State, @BuyerId) returning id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        var orderId = await db.Connection.QueryFirstOrDefaultAsync<long>(insertOrderQuery, new
        {
            request.CreationDate,
            request.From,
            request.To,
            Status = OrderStatus.Created.ToString(),
            request.BuyerId
        });

        // Привязка артиклов к заказу

        const string insertItemsInOrderQuery =
            $@"INSERT INTO {SqlConstants.ArticlesInOrder} 
                    (article_id, order_id)
            VALUES (@ArticleId, @OrderId) returning id";

        foreach (var article in request.Articles)
        {
            await db.Connection.ExecuteAsync(insertItemsInOrderQuery, new
            {
                ArticleId = article,
                OrderId = orderId
            });
        }

        return orderId;
    }

    public async Task SetOrderStatus(SetOrderStatusQuery request)
    {
        const string moveOrderStatusQuery = $@"UPDATE {SqlConstants.Orders}  
                                                SET state = @Status::state
                                                WHERE id = @OrderId ";
        await using var db = _connectionFactory.CreateDatabase();
        await db.Connection.ExecuteAsync(moveOrderStatusQuery, new
        {
            request.OrderId,
            Status = request.Status.ToString()
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
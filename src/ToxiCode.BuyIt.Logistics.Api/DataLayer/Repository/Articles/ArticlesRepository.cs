using Dapper;
using Dtos;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles.Queries;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;

public class ArticlesRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ArticlesRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<Article>> GetArticles()
    {
        const string getArticleQuery = $@"SELECT id, item_id FROM {SqlConstants.Articles}";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<Article>(db.CreateCommand(getArticleQuery));
    }

    public async Task CreateArticleByItemId(CreateArticleByItemIdQuery request, CancellationToken cancellationToken)
    {
        const string insertArticlesQuery =
            $@"INSERT INTO {SqlConstants.Articles}
                    (item_id)
            VALUES (@ItemId)";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);
        await db.Connection.OpenAsync(cancellationToken);
        var transaction = await db.Connection.BeginTransactionAsync(cancellationToken);

        for(var i = 0; i < request.Count; i++)
            await db.Connection.ExecuteAsync(insertArticlesQuery, new
                {
                    request.ItemId
                },
                transaction);
        await transaction.CommitAsync(cancellationToken);
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Dtos;
using Dtos.Items;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Articles.Commands;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;

public class ArticlesRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ArticlesRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;
    
    public async Task<IEnumerable<Article>> GetItems()
    {
        const string getArticleQuery = $@"SELECT id, item_id FROM {SqlConstants.Articles}";
        await using var db = _connectionFactory.CreateDatabase();
        return await db.Connection.QueryAsync<Article>(db.CreateCommand(getArticleQuery));
    }
    
    public async Task<long[]> CreateArticles(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        const string insertArticlesQuery =
            $@"INSERT INTO {SqlConstants.Articles} 
                    (item_id)
            VALUES (@ItemId) returning id";

        await using var db = _connectionFactory.CreateDatabase(cancellationToken);

        var articlesId = await db.Connection.QueryFirstOrDefaultAsync<long[]>(insertArticlesQuery, new
        {
            request.ItemId
        });
        return articlesId;
    }
}
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Articles;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Items;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Repository.Orders;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Utils;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Extensions;
/// <summary>
/// Database infrastructure extensions
/// </summary>
public static class InfrastructureExtension
{
    /// <summary>
    /// Add all required services to DI
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Program configuration</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
            .AddSingleton<DbExecuteWrapper>()
            .AddSingleton<ItemsRepository>()
            .AddSingleton<ArticlesRepository>()
            .AddSingleton<OrdersRepository>();

        return services.AddFluentMigratorCore()
            .ConfigureRunner(x
                => x.AddPostgres()
                    .WithGlobalConnectionString(Environment.GetEnvironmentVariable("DATABASE_STRING"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(y => y.AddFluentMigratorConsole());
    }
}
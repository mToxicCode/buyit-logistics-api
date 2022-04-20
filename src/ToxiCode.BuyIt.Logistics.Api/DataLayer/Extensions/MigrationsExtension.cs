using FluentMigrator.Runner;

namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Extensions;


public static class MigrationsExtensions
{
    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner!.ListMigrations();
        runner.MigrateUp();
        return app;
    }
}
using FluentMigrator;
using JetBrains.Annotations;


namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Migrations;

[Migration(0)]
[UsedImplicitly]
public class InitMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(SqlConstants.Items)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString(1000).NotNullable().Unique()
            .WithColumn("price").AsDecimal()
            .WithColumn("weight").AsDecimal();

        Create.Table(SqlConstants.Places)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("address").AsString(5000).NotNullable().Unique();

        Create.Table(SqlConstants.Articles)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("item_id").AsInt64();

        Create.Table(SqlConstants.Orders)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("date_time").AsDateTime()
            .WithColumn("from").AsInt64().ForeignKey(SqlConstants.Places, "id").NotNullable()
            .WithColumn("to").AsInt64().ForeignKey(SqlConstants.Places, "id").NotNullable();

        Create.Table(SqlConstants.ArticlesInOrder)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("article_id").AsInt64().ForeignKey(SqlConstants.Articles, "id").NotNullable()
            .WithColumn("order_id").AsInt64().ForeignKey(SqlConstants.Orders, "id").NotNullable();
    }
}
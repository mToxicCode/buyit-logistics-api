using FluentMigrator;
using JetBrains.Annotations;


namespace ToxiCode.BuyIt.Logistics.Api.DataLayer.Migrations;

[Migration(0)]
[UsedImplicitly]
public class InitMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql($"CREATE TYPE State AS ENUM ('Default', 'Created', 'Processing', 'Forming', 'Formed', 'Delivering', 'Delivered', 'Cancelled');");
        Create.Table(SqlConstants.Items)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("seller_id").AsGuid()
            .WithColumn("item_name").AsString(1000).NotNullable()
            .WithColumn("weight").AsDecimal()
            .WithColumn("height").AsDecimal()
            .WithColumn("length").AsDecimal()
            .WithColumn("width").AsDecimal()
            .WithColumn("creation_date").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("changed_at").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("img_url").AsString().WithDefaultValue("https://www.ceph.txcd.xyz/toxicode-buyit-api/7f2c3f68-af2f-47b6-9628-40fa98906d30.png");

        Create.Table(SqlConstants.Places)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("address").AsString(5000).NotNullable().Unique();

        Create.Table(SqlConstants.Articles)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("item_id").AsInt64().ForeignKey(SqlConstants.Items, "id");

        Create.Table(SqlConstants.Orders)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("creation_date").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("from").AsInt64().ForeignKey(SqlConstants.Places, "id").NotNullable()
            .WithColumn("to").AsInt64().ForeignKey(SqlConstants.Places, "id").NotNullable()
            .WithColumn("state").AsCustom("State")
            .WithColumn("buyer_id").AsString(5000).NotNullable();

        Create.Table(SqlConstants.ArticlesInOrder)
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("article_id").AsInt64().ForeignKey(SqlConstants.Articles, "id").NotNullable()
            .WithColumn("order_id").AsInt64().ForeignKey(SqlConstants.Orders, "id").NotNullable();
    }
}
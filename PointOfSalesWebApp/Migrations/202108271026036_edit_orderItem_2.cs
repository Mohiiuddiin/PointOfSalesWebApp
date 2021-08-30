namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_orderItem_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderItems", "ProductId");
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "Id");
        }
    }
}

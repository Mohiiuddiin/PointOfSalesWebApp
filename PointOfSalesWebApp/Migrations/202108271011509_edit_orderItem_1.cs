namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_orderItem_1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderItems", name: "OrderId", newName: "Order_Id");
            RenameIndex(table: "dbo.OrderItems", name: "IX_OrderId", newName: "IX_Order_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrderItems", name: "IX_Order_Id", newName: "IX_OrderId");
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderId");
        }
    }
}

namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockoutllk2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PurchaseDetails", new[] { "Purchase_Id" });
            DropColumn("dbo.PurchaseDetails", "PurchaseId");
            RenameColumn(table: "dbo.PurchaseDetails", name: "Purchase_Id", newName: "PurchaseId");
            AlterColumn("dbo.PurchaseDetails", "PurchaseId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PurchaseDetails", "PurchaseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            AlterColumn("dbo.PurchaseDetails", "PurchaseId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PurchaseDetails", name: "PurchaseId", newName: "Purchase_Id");
            AddColumn("dbo.PurchaseDetails", "PurchaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseDetails", "Purchase_Id");
        }
    }
}

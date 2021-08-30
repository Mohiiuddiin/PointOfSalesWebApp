namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockoutllk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseDetails", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseDetails", "Id", "dbo.Purchases");
            DropIndex("dbo.PurchaseDetails", new[] { "Id" });
            DropPrimaryKey("dbo.PurchaseDetails");
            AddColumn("dbo.PurchaseDetails", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PurchaseDetails", "Purchase_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseDetails", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PurchaseDetails", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PurchaseDetails", "Id");
            CreateIndex("dbo.PurchaseDetails", "ProductId");
            CreateIndex("dbo.PurchaseDetails", "ApplicationUser_Id");
            CreateIndex("dbo.PurchaseDetails", "Purchase_Id");
            AddForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseDetails", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PurchaseDetails", "Purchase_Id", "dbo.Purchases", "Id");
            DropColumn("dbo.PurchaseDetails", "PurchaseDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseDetails", "PurchaseDetailId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PurchaseDetails", "Purchase_Id", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchaseDetails", new[] { "Purchase_Id" });
            DropIndex("dbo.PurchaseDetails", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropPrimaryKey("dbo.PurchaseDetails");
            AlterColumn("dbo.PurchaseDetails", "Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PurchaseDetails", "ProductId", c => c.String(nullable: false));
            DropColumn("dbo.PurchaseDetails", "Purchase_Id");
            DropColumn("dbo.PurchaseDetails", "ApplicationUser_Id");
            AddPrimaryKey("dbo.PurchaseDetails", "PurchaseDetailId");
            CreateIndex("dbo.PurchaseDetails", "Id");
            AddForeignKey("dbo.PurchaseDetails", "Id", "dbo.Purchases", "Id");
            AddForeignKey("dbo.PurchaseDetails", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}

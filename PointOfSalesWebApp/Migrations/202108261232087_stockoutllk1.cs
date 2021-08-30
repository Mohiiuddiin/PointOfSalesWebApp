namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockoutllk1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "Supplier_Id", "dbo.Suppliers");
            DropIndex("dbo.Purchases", new[] { "Supplier_Id" });
            DropColumn("dbo.Purchases", "SupplierId");
            RenameColumn(table: "dbo.Purchases", name: "Supplier_Id", newName: "SupplierId");
            AlterColumn("dbo.Purchases", "SupplierId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Purchases", "SupplierId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Purchases", "SupplierId");
            AddForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            AlterColumn("dbo.Purchases", "SupplierId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Purchases", "SupplierId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Purchases", name: "SupplierId", newName: "Supplier_Id");
            AddColumn("dbo.Purchases", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "Supplier_Id");
            AddForeignKey("dbo.Purchases", "Supplier_Id", "dbo.Suppliers", "Id");
        }
    }
}

namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sales_edit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Sales", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.Attendances", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Employees", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Positions", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.BusketItems", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Buskets", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Customers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.OrderItems", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.ProductCategories", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.ProductCompanies", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Products", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.PurchaseDetails", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Purchases", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Suppliers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.Sales", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.SalesDetails", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Attendances", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Employees", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Positions", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.BusketItems", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Buskets", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Customers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.OrderItems", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.ProductCategories", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.ProductCompanies", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Products", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.PurchaseDetails", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Purchases", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Suppliers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.Sales", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.SalesDetails", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            DropColumn("dbo.Sales", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "EmployeeId", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.SalesDetails", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Sales", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Suppliers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Purchases", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.PurchaseDetails", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Products", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ProductCompanies", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ProductCategories", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.OrderItems", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Customers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Buskets", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.BusketItems", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Positions", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Employees", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Attendances", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.SalesDetails", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Sales", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Suppliers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Purchases", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.PurchaseDetails", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Products", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ProductCompanies", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ProductCategories", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.OrderItems", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Customers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Buskets", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.BusketItems", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Positions", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Employees", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Attendances", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Sales", "EmployeeId");
            AddForeignKey("dbo.Sales", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}

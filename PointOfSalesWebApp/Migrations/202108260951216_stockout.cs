namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        PurchaseDetailId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.String(nullable: false),
                        ProductName = c.String(),
                        BarCode = c.Int(nullable: false),
                        BarcodeImage = c.Binary(),
                        ImageUrl = c.String(),
                        Quantity = c.Int(nullable: false),
                        StockQuantity = c.Int(nullable: false),
                        PRate = c.Double(nullable: false),
                        SRate = c.Double(nullable: false),
                        Vat = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Id = c.String(maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseDetailId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceNo = c.String(nullable: false, maxLength: 100),
                        SupplierId = c.Int(nullable: false),
                        Remarks = c.String(maxLength: 100),
                        PaidAmount = c.Double(nullable: false),
                        DueAmount = c.Double(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Supplier_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SalesInvoiceNo = c.String(nullable: false, maxLength: 100),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        Remarks = c.String(maxLength: 100),
                        OverallDiscount = c.Double(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SaleId = c.String(maxLength: 128),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        BarCode = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SRate = c.Double(nullable: false),
                        Vat = c.Double(nullable: false),
                        LineDiscount = c.Double(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Attendances", "CreatedBy", c => c.String());
            AddColumn("dbo.Attendances", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "CreatedBy", c => c.String());
            AddColumn("dbo.Employees", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Positions", "CreatedBy", c => c.String());
            AddColumn("dbo.Positions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.BusketItems", "CreatedBy", c => c.String());
            AddColumn("dbo.BusketItems", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Buskets", "CreatedBy", c => c.String());
            AddColumn("dbo.Buskets", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "CreatedBy", c => c.String());
            AddColumn("dbo.Customers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.OrderItems", "CreatedBy", c => c.String());
            AddColumn("dbo.OrderItems", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "CreatedBy", c => c.String());
            AddColumn("dbo.Orders", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ProductCategories", "CreatedBy", c => c.String());
            AddColumn("dbo.ProductCategories", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ProductCompanies", "CreatedBy", c => c.String());
            AddColumn("dbo.ProductCompanies", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "CreatedBy", c => c.String());
            AddColumn("dbo.Products", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Attendances", "ApplicationUser_Id");
            CreateIndex("dbo.Employees", "ApplicationUser_Id");
            CreateIndex("dbo.Positions", "ApplicationUser_Id");
            CreateIndex("dbo.BusketItems", "ApplicationUser_Id");
            CreateIndex("dbo.Buskets", "ApplicationUser_Id");
            CreateIndex("dbo.Customers", "ApplicationUser_Id");
            CreateIndex("dbo.OrderItems", "ApplicationUser_Id");
            CreateIndex("dbo.Orders", "ApplicationUser_Id");
            CreateIndex("dbo.ProductCategories", "ApplicationUser_Id");
            CreateIndex("dbo.ProductCompanies", "ApplicationUser_Id");
            CreateIndex("dbo.Products", "ApplicationUser_Id");
            AddForeignKey("dbo.Attendances", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Positions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BusketItems", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Buskets", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.OrderItems", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProductCategories", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProductCompanies", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SalesDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SalesDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Purchases", "Supplier_Id", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseDetails", "Id", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseDetails", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductCompanies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductCategories", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Buskets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BusketItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Positions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SalesDetails", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SalesDetails", new[] { "ProductId" });
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.Sales", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Sales", new[] { "EmployeeId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Suppliers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Purchases", new[] { "Supplier_Id" });
            DropIndex("dbo.Purchases", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PurchaseDetails", new[] { "Id" });
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProductCompanies", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProductCategories", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Orders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrderItems", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Customers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Buskets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.BusketItems", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Positions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Employees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Attendances", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Products", "ApplicationUser_Id");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.ProductCompanies", "ApplicationUser_Id");
            DropColumn("dbo.ProductCompanies", "CreatedBy");
            DropColumn("dbo.ProductCategories", "ApplicationUser_Id");
            DropColumn("dbo.ProductCategories", "CreatedBy");
            DropColumn("dbo.Orders", "ApplicationUser_Id");
            DropColumn("dbo.Orders", "CreatedBy");
            DropColumn("dbo.OrderItems", "ApplicationUser_Id");
            DropColumn("dbo.OrderItems", "CreatedBy");
            DropColumn("dbo.Customers", "ApplicationUser_Id");
            DropColumn("dbo.Customers", "CreatedBy");
            DropColumn("dbo.Buskets", "ApplicationUser_Id");
            DropColumn("dbo.Buskets", "CreatedBy");
            DropColumn("dbo.BusketItems", "ApplicationUser_Id");
            DropColumn("dbo.BusketItems", "CreatedBy");
            DropColumn("dbo.Positions", "ApplicationUser_Id");
            DropColumn("dbo.Positions", "CreatedBy");
            DropColumn("dbo.Employees", "ApplicationUser_Id");
            DropColumn("dbo.Employees", "CreatedBy");
            DropColumn("dbo.Attendances", "ApplicationUser_Id");
            DropColumn("dbo.Attendances", "CreatedBy");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Sales");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseDetails");
        }
    }
}

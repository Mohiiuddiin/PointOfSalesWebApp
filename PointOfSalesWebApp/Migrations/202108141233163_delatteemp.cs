namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delatteemp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Attendances", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Attendances", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropTable("dbo.Attendances");
            DropTable("dbo.Employees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeIdNo = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Image = c.String(),
                        PositionId = c.String(maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        CheckTime = c.DateTime(nullable: false),
                        CheckStatus = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Employees", "PositionId");
            CreateIndex("dbo.Attendances", "EmployeeId");
            AddForeignKey("dbo.Attendances", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id");
        }
    }
}

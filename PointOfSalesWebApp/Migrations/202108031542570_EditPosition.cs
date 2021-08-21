namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPosition : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Positions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        PositionId = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}

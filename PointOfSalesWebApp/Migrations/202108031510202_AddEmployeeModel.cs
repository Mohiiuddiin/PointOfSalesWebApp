namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeModel : DbMigration
    {
        public override void Up()
        {
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
                        PositionId = c.String(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                        Position_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Position_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "Position_Id" });
            DropTable("dbo.Positions");
            DropTable("dbo.Employees");
        }
    }
}

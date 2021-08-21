namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattendancemodel : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Attendances", new[] { "EmployeeId" });
            DropTable("dbo.Attendances");
        }
    }
}

namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editemp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            AddColumn("dbo.Customers", "MobileNumber", c => c.String());
            AlterColumn("dbo.Employees", "PositionId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            AlterColumn("dbo.Employees", "PositionId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Customers", "MobileNumber");
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}

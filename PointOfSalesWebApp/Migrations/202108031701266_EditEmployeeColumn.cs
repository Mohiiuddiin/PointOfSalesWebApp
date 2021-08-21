namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEmployeeColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Image", c => c.String(nullable: false));
        }
    }
}

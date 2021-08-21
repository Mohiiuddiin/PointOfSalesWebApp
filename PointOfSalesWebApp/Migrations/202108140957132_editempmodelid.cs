namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editempmodelid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeIdNo", c => c.String());
            DropColumn("dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EmployeeId", c => c.String());
            DropColumn("dbo.Employees", "EmployeeIdNo");
        }
    }
}

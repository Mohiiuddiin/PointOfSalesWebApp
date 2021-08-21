namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editemployee1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "EmployeeIdNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EmployeeIdNo", c => c.String());
        }
    }
}

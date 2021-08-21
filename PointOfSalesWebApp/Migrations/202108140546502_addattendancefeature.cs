namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattendancefeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmployeeId");
        }
    }
}

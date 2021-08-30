namespace PointOfSalesWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_order_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TransNo", c => c.String());
            AddColumn("dbo.Orders", "TransAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TransAmount");
            DropColumn("dbo.Orders", "TransNo");
        }
    }
}

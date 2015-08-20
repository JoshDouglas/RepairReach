namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLaborLineItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LineItems", "LaborQty");
            DropColumn("dbo.LineItems", "LaborEach");
            DropColumn("dbo.LineItems", "LaborCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LineItems", "LaborCost", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LineItems", "LaborEach", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.LineItems", "LaborQty", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}

namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuickLineItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuickLineItems",
                c => new
                    {
                        QuickLineItemId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PartName = c.String(),
                        PartQty = c.Decimal(precision: 18, scale: 2),
                        PartEach = c.Decimal(precision: 18, scale: 2),
                        PartCost = c.Decimal(precision: 18, scale: 2),
                        PartNumber = c.String(),
                        ServiceName = c.String(),
                        ServiceQty = c.Decimal(precision: 18, scale: 2),
                        ServiceEach = c.Decimal(precision: 18, scale: 2),
                        ServiceCost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.QuickLineItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuickLineItems");
        }
    }
}

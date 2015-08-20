namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorApplianceToJob : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appliances", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.LineItems", "ApplianceId", "dbo.Appliances");
            DropIndex("dbo.Appliances", new[] { "CustomerId" });
            DropIndex("dbo.LineItems", new[] { "ApplianceId" });
            AddColumn("dbo.Appliances", "JobId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appliances", "JobId");
            AddForeignKey("dbo.Appliances", "JobId", "dbo.Jobs", "JobId", cascadeDelete: true);
            DropColumn("dbo.LineItems", "ApplianceId");
            DropColumn("dbo.Appliances", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appliances", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.LineItems", "ApplianceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Appliances", "JobId", "dbo.Jobs");
            DropIndex("dbo.Appliances", new[] { "JobId" });
            DropColumn("dbo.Appliances", "JobId");
            CreateIndex("dbo.LineItems", "ApplianceId");
            CreateIndex("dbo.Appliances", "CustomerId");
            AddForeignKey("dbo.LineItems", "ApplianceId", "dbo.Appliances", "ApplianceId", cascadeDelete: true);
            AddForeignKey("dbo.Appliances", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}

namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectAndCallToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CollectPaymentOnSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "CallOnWay", c => c.Boolean(nullable: false));
            DropColumn("dbo.Jobs", "CollectPaymentOnSite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CollectPaymentOnSite", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "CallOnWay");
            DropColumn("dbo.Customers", "CollectPaymentOnSite");
        }
    }
}

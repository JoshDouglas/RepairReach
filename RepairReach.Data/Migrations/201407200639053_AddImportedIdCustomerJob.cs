namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImportedIdCustomerJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ImportedJobId", c => c.Int());
            AddColumn("dbo.Customers", "ImportedCustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ImportedCustomerId");
            DropColumn("dbo.Jobs", "ImportedJobId");
        }
    }
}

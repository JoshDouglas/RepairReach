namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentMethodDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentMethods", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentMethods", "Description", c => c.String());
        }
    }
}

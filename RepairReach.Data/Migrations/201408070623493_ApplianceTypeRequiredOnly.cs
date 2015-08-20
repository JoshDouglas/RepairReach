namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplianceTypeRequiredOnly : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appliances", "Make", c => c.String());
            AlterColumn("dbo.Appliances", "ModelNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appliances", "ModelNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Appliances", "Make", c => c.String(nullable: false));
        }
    }
}

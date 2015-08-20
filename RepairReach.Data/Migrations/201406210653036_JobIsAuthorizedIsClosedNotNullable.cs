namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobIsAuthorizedIsClosedNotNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "IsAuthorized", c => c.Boolean());
            AlterColumn("dbo.Jobs", "IsClosed", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "IsClosed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Jobs", "IsAuthorized", c => c.Boolean(nullable: false));
        }
    }
}

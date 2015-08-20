namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobAddedIsAuthorizedIsClosed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "IsAuthorized", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "IsClosed");
            DropColumn("dbo.Jobs", "IsAuthorized");
        }
    }
}

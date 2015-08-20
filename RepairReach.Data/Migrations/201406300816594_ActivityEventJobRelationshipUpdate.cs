namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityEventJobRelationshipUpdate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ActivityEvents", "JobId");
            AddForeignKey("dbo.ActivityEvents", "JobId", "dbo.Jobs", "JobId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityEvents", "JobId", "dbo.Jobs");
            DropIndex("dbo.ActivityEvents", new[] { "JobId" });
        }
    }
}

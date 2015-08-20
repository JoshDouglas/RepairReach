namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastViewedTimeJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "LastViewedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "LastViewedBy", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "LastViewedBy");
            DropColumn("dbo.Jobs", "LastViewedTime");
        }
    }
}

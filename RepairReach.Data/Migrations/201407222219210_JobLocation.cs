namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Location_lat", c => c.Double(nullable: false));
            AddColumn("dbo.Jobs", "Location_lng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Location_lng");
            DropColumn("dbo.Jobs", "Location_lat");
        }
    }
}

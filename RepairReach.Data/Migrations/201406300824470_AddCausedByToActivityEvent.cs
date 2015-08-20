namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCausedByToActivityEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityEvents", "CausedBy", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityEvents", "CausedBy");
        }
    }
}

namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivityEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityEvents",
                c => new
                    {
                        ActivityEventId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        EventTime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityEventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActivityEvents");
        }
    }
}

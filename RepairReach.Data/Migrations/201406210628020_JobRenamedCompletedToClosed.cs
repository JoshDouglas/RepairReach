namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobRenamedCompletedToClosed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobClosed", c => c.DateTime());
            DropColumn("dbo.Jobs", "JobCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobCompleted", c => c.DateTime());
            DropColumn("dbo.Jobs", "JobClosed");
        }
    }
}

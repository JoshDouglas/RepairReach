namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsClosedFromJobTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "IsClosed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "IsClosed", c => c.Boolean());
        }
    }
}

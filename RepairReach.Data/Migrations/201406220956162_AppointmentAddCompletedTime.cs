namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentAddCompletedTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsCompleted", c => c.Boolean());
            AddColumn("dbo.Appointments", "CompletedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "CompletedTime");
            DropColumn("dbo.Appointments", "IsCompleted");
        }
    }
}

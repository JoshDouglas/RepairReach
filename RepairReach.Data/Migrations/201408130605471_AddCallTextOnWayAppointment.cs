namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCallTextOnWayAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "CallOnWay", c => c.Boolean(nullable: false));
            AddColumn("dbo.Appointments", "TextOnWay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "TextOnWay");
            DropColumn("dbo.Appointments", "CallOnWay");
        }
    }
}

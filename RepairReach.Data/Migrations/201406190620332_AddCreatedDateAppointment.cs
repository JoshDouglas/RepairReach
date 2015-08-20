namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Created");
        }
    }
}

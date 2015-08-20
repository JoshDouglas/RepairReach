namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffObjectsAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "TechnicianStaffId", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "CreatedBy", c => c.String(nullable: false));
            CreateIndex("dbo.Appointments", "TechnicianStaffId");
            AddForeignKey("dbo.Appointments", "TechnicianStaffId", "dbo.Staffs", "StaffId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "TechnicianStaffId", "dbo.Staffs");
            DropIndex("dbo.Appointments", new[] { "TechnicianStaffId" });
            DropColumn("dbo.Appointments", "CreatedBy");
            DropColumn("dbo.Appointments", "TechnicianStaffId");
        }
    }
}

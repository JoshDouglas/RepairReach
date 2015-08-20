namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesRepStatusRequiredJob : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "JobStatusId", "dbo.JobStatus");
            DropForeignKey("dbo.Jobs", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Jobs", new[] { "JobStatusId" });
            DropIndex("dbo.Jobs", new[] { "StaffId" });
            AlterColumn("dbo.Jobs", "JobStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "JobStatusId");
            CreateIndex("dbo.Jobs", "StaffId");
            AddForeignKey("dbo.Jobs", "JobStatusId", "dbo.JobStatus", "JobStatusId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "StaffId", "dbo.Staffs", "StaffId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Jobs", "JobStatusId", "dbo.JobStatus");
            DropIndex("dbo.Jobs", new[] { "StaffId" });
            DropIndex("dbo.Jobs", new[] { "JobStatusId" });
            AlterColumn("dbo.Jobs", "StaffId", c => c.Int());
            AlterColumn("dbo.Jobs", "JobStatusId", c => c.Int());
            CreateIndex("dbo.Jobs", "StaffId");
            CreateIndex("dbo.Jobs", "JobStatusId");
            AddForeignKey("dbo.Jobs", "StaffId", "dbo.Staffs", "StaffId");
            AddForeignKey("dbo.Jobs", "JobStatusId", "dbo.JobStatus", "JobStatusId");
        }
    }
}

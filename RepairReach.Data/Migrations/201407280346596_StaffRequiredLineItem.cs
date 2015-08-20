namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaffRequiredLineItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LineItems", "StaffId", "dbo.Staffs");
            DropIndex("dbo.LineItems", new[] { "StaffId" });
            AlterColumn("dbo.LineItems", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.LineItems", "StaffId");
            AddForeignKey("dbo.LineItems", "StaffId", "dbo.Staffs", "StaffId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineItems", "StaffId", "dbo.Staffs");
            DropIndex("dbo.LineItems", new[] { "StaffId" });
            AlterColumn("dbo.LineItems", "StaffId", c => c.Int());
            CreateIndex("dbo.LineItems", "StaffId");
            AddForeignKey("dbo.LineItems", "StaffId", "dbo.Staffs", "StaffId");
        }
    }
}

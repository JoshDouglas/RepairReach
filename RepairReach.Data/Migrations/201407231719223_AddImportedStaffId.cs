namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImportedStaffId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "ImportedStaffId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "ImportedStaffId");
        }
    }
}

namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaffDisplayName50Characters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "DisplayName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "DisplayName", c => c.String(nullable: false, maxLength: 15));
        }
    }
}

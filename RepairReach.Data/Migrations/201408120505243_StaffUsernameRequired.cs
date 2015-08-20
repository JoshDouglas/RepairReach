namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaffUsernameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "Username", c => c.String());
        }
    }
}

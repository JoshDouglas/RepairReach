namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "Username");
        }
    }
}

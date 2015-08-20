namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeZoneInfoToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "TimeZoneInfo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "TimeZoneInfo");
        }
    }
}

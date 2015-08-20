namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyLogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "LogoPath", c => c.String());
            DropColumn("dbo.Companies", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Logo", c => c.Byte());
            DropColumn("dbo.Companies", "LogoPath");
        }
    }
}

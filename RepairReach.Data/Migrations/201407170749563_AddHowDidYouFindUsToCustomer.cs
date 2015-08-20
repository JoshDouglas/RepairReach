namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHowDidYouFindUsToCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", "dbo.HowDidYouFindUs");
            DropIndex("dbo.Customers", new[] { "HowDidYouFindUs_HowDidYouFindUsId" });
            AddColumn("dbo.Customers", "HowDidYouFindUsId", c => c.Int());
            CreateIndex("dbo.Customers", "HowDidYouFindUsId");
            AddForeignKey("dbo.Customers", "HowDidYouFindUsId", "dbo.HowDidYouFindUs", "HowDidYouFindUsId");
            DropColumn("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", c => c.Int());
            DropForeignKey("dbo.Customers", "HowDidYouFindUsId", "dbo.HowDidYouFindUs");
            DropIndex("dbo.Customers", new[] { "HowDidYouFindUsId" });
            DropColumn("dbo.Customers", "HowDidYouFindUsId");
            CreateIndex("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId");
            AddForeignKey("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", "dbo.HowDidYouFindUs", "HowDidYouFindUsId");
        }
    }
}

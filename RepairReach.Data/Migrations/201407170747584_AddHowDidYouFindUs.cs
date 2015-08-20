namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHowDidYouFindUs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HowDidYouFindUs",
                c => new
                    {
                        HowDidYouFindUsId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        SequenceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HowDidYouFindUsId);
            
            AddColumn("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", c => c.Int());
            CreateIndex("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId");
            AddForeignKey("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", "dbo.HowDidYouFindUs", "HowDidYouFindUsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId", "dbo.HowDidYouFindUs");
            DropIndex("dbo.Customers", new[] { "HowDidYouFindUs_HowDidYouFindUsId" });
            DropColumn("dbo.Customers", "HowDidYouFindUs_HowDidYouFindUsId");
            DropTable("dbo.HowDidYouFindUs");
        }
    }
}

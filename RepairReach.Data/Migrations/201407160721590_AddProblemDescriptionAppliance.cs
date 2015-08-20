namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProblemDescriptionAppliance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appliances", "ProblemDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appliances", "ProblemDescription");
        }
    }
}

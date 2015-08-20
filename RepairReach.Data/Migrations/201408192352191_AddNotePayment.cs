namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotePayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Note");
        }
    }
}

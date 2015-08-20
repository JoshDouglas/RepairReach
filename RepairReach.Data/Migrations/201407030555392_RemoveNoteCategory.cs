namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNoteCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JobNotes", "NoteCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobNotes", "NoteCategory", c => c.Int(nullable: false));
        }
    }
}

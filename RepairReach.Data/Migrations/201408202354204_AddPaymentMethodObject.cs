namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentMethodObject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        PaymentMethodId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        SequenceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentMethodId);
            
            AddColumn("dbo.Payments", "PaymentMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "PaymentMethodId");
            AddForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods", "PaymentMethodId");
            DropColumn("dbo.Payments", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PaymentMethod", c => c.String(nullable: false));
            DropForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Payments", new[] { "PaymentMethodId" });
            DropColumn("dbo.Payments", "PaymentMethodId");
            DropTable("dbo.PaymentMethods");
        }
    }
}

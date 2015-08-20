namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appliances",
                c => new
                    {
                        ApplianceId = c.Int(nullable: false, identity: true),
                        Make = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        ModelNumber = c.String(nullable: false),
                        SerialNumber = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplianceId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Designation = c.Int(nullable: false),
                        CompanyName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone1 = c.String(nullable: false),
                        Phone2 = c.String(),
                        Email = c.String(),
                        Fax = c.String(),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        PrefersTextMessaging = c.Boolean(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        JobNumber = c.Int(nullable: false),
                        JobStatusId = c.Int(),
                        JobCategoryId = c.Int(),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        ContactFirstName = c.String(nullable: false),
                        ContactLastName = c.String(nullable: false),
                        ContactPhone1 = c.String(nullable: false),
                        ContactPhone2 = c.String(),
                        Description = c.String(nullable: false),
                        CollectPaymentOnSite = c.Boolean(nullable: false),
                        JobCreated = c.DateTime(nullable: false),
                        JobAuthorized = c.DateTime(),
                        JobScheduled = c.DateTime(),
                        JobStarted = c.DateTime(),
                        JobFinished = c.DateTime(),
                        JobCompleted = c.DateTime(),
                        JobBilled = c.DateTime(),
                        StaffId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.JobCategories", t => t.JobCategoryId)
                .ForeignKey("dbo.JobStatus", t => t.JobStatusId)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.CustomerId)
                .Index(t => t.JobCategoryId)
                .Index(t => t.JobStatusId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.JobCategories",
                c => new
                    {
                        JobCategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        SequenceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobCategoryId);
            
            CreateTable(
                "dbo.JobNotes",
                c => new
                    {
                        JobNoteId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        NoteCategory = c.Int(nullable: false),
                        Note = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JobNoteId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.JobStatus",
                c => new
                    {
                        JobStatusId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        SequenceNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobStatusId);
            
            CreateTable(
                "dbo.LineItems",
                c => new
                    {
                        LineItemId = c.Int(nullable: false, identity: true),
                        LineItemNumber = c.Int(nullable: false),
                        Description = c.String(),
                        JobId = c.Int(nullable: false),
                        ApplianceId = c.Int(nullable: false),
                        PartName = c.String(),
                        PartQty = c.Decimal(precision: 18, scale: 2),
                        PartEach = c.Decimal(precision: 18, scale: 2),
                        PartCost = c.Decimal(precision: 18, scale: 2),
                        PartNumber = c.String(),
                        ServiceName = c.String(),
                        ServiceQty = c.Decimal(precision: 18, scale: 2),
                        ServiceEach = c.Decimal(precision: 18, scale: 2),
                        ServiceCost = c.Decimal(precision: 18, scale: 2),
                        LaborQty = c.Decimal(precision: 18, scale: 2),
                        LaborEach = c.Decimal(precision: 18, scale: 2),
                        LaborCost = c.Decimal(precision: 18, scale: 2),
                        StaffId = c.Int(),
                        TaxRateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LineItemId)
                .ForeignKey("dbo.Appliances", t => t.ApplianceId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .ForeignKey("dbo.TaxRates", t => t.TaxRateId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.ApplianceId)
                .Index(t => t.JobId)
                .Index(t => t.TaxRateId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.TaxRates",
                c => new
                    {
                        TaxRateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDefaultRate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TaxRateId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 15),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserTitle = c.Int(),
                        Phone = c.String(),
                        Email = c.String(),
                        HourlyRate = c.Decimal(precision: 18, scale: 2),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.TimeClockEntries",
                c => new
                    {
                        TimeClockEntryId = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        HourlyRate = c.Decimal(precision: 18, scale: 2),
                        TimeIn = c.DateTime(),
                        TimeOut = c.DateTime(),
                        DatePaid = c.DateTime(),
                    })
                .PrimaryKey(t => t.TimeClockEntryId)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentMethod = c.String(nullable: false),
                        PaymentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DatePaid = c.DateTime(nullable: false),
                        EnteredBy = c.String(nullable: false),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Website = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        Logo = c.Byte(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostAmount = c.Decimal(precision: 18, scale: 2),
                        PartNumber = c.String(),
                    })
                .PrimaryKey(t => t.PartId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostAmount = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        StaffId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        CompanyPhone = c.String(),
                        CompanyEmail = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Contact1Name = c.String(),
                        Contact1Title = c.String(),
                        Contact1Phone = c.String(),
                        Contact1Email = c.String(),
                        Contact2Name = c.String(),
                        Contact2Title = c.String(),
                        Contact2Phone = c.String(),
                        Contact2Email = c.String(),
                    })
                .PrimaryKey(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appliances", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Jobs", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Payments", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.LineItems", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.TimeClockEntries", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.LineItems", "TaxRateId", "dbo.TaxRates");
            DropForeignKey("dbo.LineItems", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.LineItems", "ApplianceId", "dbo.Appliances");
            DropForeignKey("dbo.Jobs", "JobStatusId", "dbo.JobStatus");
            DropForeignKey("dbo.JobNotes", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "JobCategoryId", "dbo.JobCategories");
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetUsers", new[] { "StaffId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "User_Id" });
            DropIndex("dbo.Appliances", new[] { "CustomerId" });
            DropIndex("dbo.Jobs", new[] { "StaffId" });
            DropIndex("dbo.Payments", new[] { "JobId" });
            DropIndex("dbo.LineItems", new[] { "StaffId" });
            DropIndex("dbo.TimeClockEntries", new[] { "StaffId" });
            DropIndex("dbo.LineItems", new[] { "TaxRateId" });
            DropIndex("dbo.LineItems", new[] { "JobId" });
            DropIndex("dbo.LineItems", new[] { "ApplianceId" });
            DropIndex("dbo.Jobs", new[] { "JobStatusId" });
            DropIndex("dbo.JobNotes", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "JobCategoryId" });
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            DropTable("dbo.Vendors");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Services");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parts");
            DropTable("dbo.Companies");
            DropTable("dbo.Payments");
            DropTable("dbo.TimeClockEntries");
            DropTable("dbo.Staffs");
            DropTable("dbo.TaxRates");
            DropTable("dbo.LineItems");
            DropTable("dbo.JobStatus");
            DropTable("dbo.JobNotes");
            DropTable("dbo.JobCategories");
            DropTable("dbo.Jobs");
            DropTable("dbo.Customers");
            DropTable("dbo.Appliances");
        }
    }
}

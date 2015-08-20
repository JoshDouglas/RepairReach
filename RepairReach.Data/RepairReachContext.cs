using System.Data.Entity;
using System.Data.Entity.Migrations.Infrastructure;
using System.Diagnostics;
using RepairReach.Core.Model;
using RepairReach.Data.Infrastructure.Identity;
using RepairReach.Data.Infrastructure.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;
using ApplicationUser = RepairReach.Data.Infrastructure.Identity.ApplicationUser;

namespace RepairReach.Data
{
    public partial class RepairReachContext : IdentityDbContext<ApplicationUser>
    {
        public RepairReachContext()
            : base("RepairReach")
        {
            Database.Log = sql => Debug.Write(sql);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Configurations.Add(new UserEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new StaffEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new ApplianceEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new CompanyEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new CustomerEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new JobCategoryEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new JobEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new JobNoteEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new JobStatusEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new LineItemEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new PartEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new PaymentEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new ServiceEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new TaxRateEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new VendorEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new TimeClockEntryEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new AppointmentEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new ActivityEventEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new HowDidYouFindUsEntityTypeConfigurator());
            modelBuilder.Configurations.Add(new PaymentMethodEntityTypeConfigurator());
            //add custom conventions
            //modelBuilder.Conventions.Add<CLRDateTimeToSqlDateTime2>();
            //modelBuilder.Conventions.Add<MaxStringLengthConvention>();

            //modelBuilder.Entity<Question>()
            //    .Property(q => q.Answer).HasMaxLength(1024);

            //Add all entity type configurations defined in "this" assembly. With this
            //method the boilerplate code to add configurations is removed.
            //modelBuilder.Configurations.AddFromAssembly(typeof(RepairReachContext).Assembly);
        }

        public DbSet<Appliance> Appliances { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<JobStatus> JobStatuses { get; set; }

        public DbSet<LineItem> LineItems { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<TaxRate> TaxRates { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<JobNote> JobNotes { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<TimeClockEntry> TimeClockEntries { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ActivityEvent> ActivityEvents { get; set; }

        public DbSet<QuickLineItem> QuickLineItems { get; set; }

        public DbSet<HowDidYouFindUs> HowDidYouFindUses { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}
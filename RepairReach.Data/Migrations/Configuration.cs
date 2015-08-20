using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using RepairReach.Data.Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RepairReach.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RepairReach.Data.RepairReachContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RepairReach.Data.RepairReachContext context)
        {
            try
            {
                //if (System.Diagnostics.Debugger.IsAttached == false)
                //    System.Diagnostics.Debugger.Launch();
                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data. E.g.

                //05.14.2014 JDD - to try and prevent duplicate seeding I'm adding this check for now
                if (context.Companies.ToListAsync().Result.Count > 0) return;

                //settings
                AddCompany(context);
                AddTaxRates(context);
                AddCategories(context);
                AddStatuses(context);
                AddQuickLineItems(context);
                AddVendors(context);
                AddHowDidYouFindUses(context);
                AddPaymentMethods(context);

                //membership
                AddStaff(context);
                AddRoles(context);
                AddUsers(context);
                
                //job example
                AddCustomers(context);
                AddJobs(context);
                AddAppliances(context);
                AddAppointments(context);
                AddLineItems(context);
                
            }
            catch (Exception ex)
            {
                SaveChanges(context);
            }
        }

        private void AddPaymentMethods(RepairReachContext context)
        {
            PaymentMethod p1 = new PaymentMethod();
            p1.SequenceNumber = 1;
            p1.Description = "Credit Card";

            PaymentMethod p2 = new PaymentMethod();
            p2.SequenceNumber = 2;
            p2.Description = "Check";

            PaymentMethod p3 = new PaymentMethod();
            p3.SequenceNumber = 3;
            p3.Description = "Cash";

            PaymentMethod p4 = new PaymentMethod();
            p4.SequenceNumber = 4;
            p4.Description = "VISA";

            PaymentMethod p5 = new PaymentMethod();
            p5.SequenceNumber = 5;
            p5.Description = "MasterCard";

            PaymentMethod p6 = new PaymentMethod();
            p6.SequenceNumber = 6;
            p6.Description = "Discover";

            PaymentMethod p7 = new PaymentMethod();
            p7.SequenceNumber = 7;
            p7.Description = "EFT";

            PaymentMethod p8 = new PaymentMethod();
            p8.SequenceNumber = 8;
            p8.Description = "Adjustment";

            context.PaymentMethods.AddOrUpdate(p1);
            context.PaymentMethods.AddOrUpdate(p2);
            context.PaymentMethods.AddOrUpdate(p3);
            context.PaymentMethods.AddOrUpdate(p4);
            context.PaymentMethods.AddOrUpdate(p5);
            context.PaymentMethods.AddOrUpdate(p6);
            context.PaymentMethods.AddOrUpdate(p7);
            context.PaymentMethods.AddOrUpdate(p8);
        }

        private void AddCompany(RepairReach.Data.RepairReachContext context)
        {
            Company company1 = new Company();
            company1.Name = "Your Company Name";
            company1.Phone = "(801) 123-1234";
            company1.Address1 = "1234 Main St.";
            company1.City = "Logan";
            company1.State = "UT";
            company1.Zipcode = "84321";
            //company1.Logo = 2;
            company1.TimeZoneInfo = "Mountain Standard Time";

            
            context.Companies.AddOrUpdate(company1);
        }

        private void AddTaxRates(RepairReach.Data.RepairReachContext context)
        {
            TaxRate taxRate1 = new TaxRate();
            taxRate1.Name = "Ogden Sales Tax";
            taxRate1.Amount = 6.85M;
            taxRate1.IsDefaultRate = false;

            TaxRate taxRate2 = new TaxRate();
            taxRate2.Name = "Logan Sales Tax";
            taxRate2.Amount = 6.60M;
            taxRate2.IsDefaultRate = true;

            
            context.TaxRates.AddOrUpdate(taxRate1);
            context.TaxRates.AddOrUpdate(taxRate2);
        }

        private void AddCategories(RepairReach.Data.RepairReachContext context)
        {
            JobCategory category1 = new JobCategory();
            category1.SequenceNumber = 1;
            category1.Description = "Warranty";

            JobCategory category2 = new JobCategory();
            category2.SequenceNumber = 2;
            category2.Description = "Walk In";

            JobCategory category3 = new JobCategory();
            category3.SequenceNumber = 3;
            category3.Description = "Phone Book";

            JobCategory category4 = new JobCategory();
            category4.SequenceNumber = 4;
            category4.Description = "Reference";

            

            context.JobCategories.AddOrUpdate(category1);
            context.JobCategories.AddOrUpdate(category2);
            context.JobCategories.AddOrUpdate(category3);
            context.JobCategories.AddOrUpdate(category4);
        }

        private void AddStatuses(RepairReach.Data.RepairReachContext context)
        {
            JobStatus status1 = new JobStatus();
            status1.SequenceNumber = 1;
            status1.Description = "On Hold";

            JobStatus status2 = new JobStatus();
            status2.SequenceNumber = 2;
            status2.Description = "Fill In";

            JobStatus status3 = new JobStatus();
            status3.SequenceNumber = 3;
            status3.Description = "Scheduled";

            JobStatus status4 = new JobStatus();
            status4.SequenceNumber = 4;
            status4.Description = "Needs Approval";

            JobStatus status5 = new JobStatus();
            status5.SequenceNumber = 5;
            status5.Description = "Parts Needed";

            JobStatus status6 = new JobStatus();
            status6.SequenceNumber = 6;
            status6.Description = "Parts Ordered";

            JobStatus status7 = new JobStatus();
            status7.SequenceNumber = 7;
            status7.Description = "Reschedule";

            JobStatus status8 = new JobStatus();
            status8.SequenceNumber = 8;
            status8.Description = "Mail Invoice";

            JobStatus status9 = new JobStatus();
            status9.SequenceNumber = 9;
            status9.Description = "Awaiting Payment";

            JobStatus status10 = new JobStatus();
            status10.SequenceNumber = 10;
            status10.Description = "Completed";

            JobStatus status11 = new JobStatus();
            status11.SequenceNumber = 11;
            status11.Description = "Closed";

            

            context.JobStatuses.AddOrUpdate(status1);
            context.JobStatuses.AddOrUpdate(status2);
            context.JobStatuses.AddOrUpdate(status3);
            context.JobStatuses.AddOrUpdate(status4);
            context.JobStatuses.AddOrUpdate(status5);
            context.JobStatuses.AddOrUpdate(status6);
            context.JobStatuses.AddOrUpdate(status7);
            context.JobStatuses.AddOrUpdate(status8);
            context.JobStatuses.AddOrUpdate(status9);
            context.JobStatuses.AddOrUpdate(status10);
            context.JobStatuses.AddOrUpdate(status11);
        }

        private void AddStaff(RepairReach.Data.RepairReachContext context)
        {
            Staff staff1 = new Staff();
            staff1.StaffId = 1;
            staff1.DisplayName = "DEMOTECH";
            staff1.FirstName = "Example";
            staff1.LastName = "Tech";
            staff1.UserTitle = Core.Enum.UserTitleEnum.Technician;
            staff1.IsActive = true;
            staff1.Username = "DEMOTECH";

            Staff staff2 = new Staff();
            staff2.StaffId = 2;
            staff2.DisplayName = "DEMODISPATCH";
            staff2.FirstName = "Example";
            staff2.LastName = "Dispatch";
            staff2.UserTitle = Core.Enum.UserTitleEnum.Dispatcher;
            staff2.IsActive = true;
            staff2.Username = "DEMODISPATCH";

            Staff staff3 = new Staff();
            staff3.StaffId = 3;
            staff3.DisplayName = "DEMOADMIN";
            staff3.FirstName = "Example";
            staff3.LastName = "Admin";
            staff3.UserTitle = Core.Enum.UserTitleEnum.Owner;
            staff3.IsActive = true;
            staff3.Username = "DEMOADMIN";

            //Staff staff3 = new Staff();
            //staff3.DisplayName = "BOBBY";
            //staff3.FirstName = "Bobby";
            //staff3.LastName = "Test";
            //staff3.UserTitle = Core.Enum.UserTitleEnum.SalesRepresentative;
            //staff3.IsActive = true;

            //Staff staff4 = new Staff();
            //staff4.DisplayName = "SALLY";
            //staff4.FirstName = "Sally";
            //staff4.LastName = "Test";
            //staff4.UserTitle = Core.Enum.UserTitleEnum.Dispatcher;
            //staff4.IsActive = true;

            context.Staff.AddOrUpdate(staff1);
            context.Staff.AddOrUpdate(staff2);
            context.Staff.AddOrUpdate(staff3);
            //context.Staff.AddOrUpdate(staff4);
        }

        private void AddCustomers(RepairReach.Data.RepairReachContext context)
        {
            Customer customer1 = new Customer();
            customer1.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer1.CompanyName = " ";
            customer1.FirstName = "John";
            customer1.LastName = "Doe";
            customer1.Phone1 = "801-123-1234";
            customer1.Address1 = "1234 Pinacle St.";
            customer1.City = "Logan";
            customer1.State = "UT";
            customer1.Zipcode = "84321";
            customer1.CollectPaymentOnSite = true;
            customer1.CallOnWay = true;

            //Customer customer2 = new Customer();
            //customer2.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            ////customer2.CompanyName = " ";
            //customer2.FirstName = "Doug";
            //customer2.LastName = "Hanson";
            //customer2.Phone1 = "801-123-1111";
            //customer2.Address1 = "321 Jefferson Ave.";
            //customer2.City = "Brigham";
            //customer2.State = "UT";
            //customer2.Zipcode = "84411";

            //Customer customer3 = new Customer();
            //customer3.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            ////customer3.CompanyName = " ";
            //customer3.FirstName = "Stan";
            //customer3.LastName = "Stanleyson";
            //customer3.Phone1 = "801-321-2323";
            //customer3.Address1 = "5677 Madison St.";
            //customer3.City = "Logan";
            //customer3.State = "UT";
            //customer3.Zipcode = "84412";

            //Customer customer4 = new Customer();
            //customer4.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            ////customer4.CompanyName = " ";
            //customer4.FirstName = "Jenny";
            //customer4.LastName = "Robinson";
            //customer4.Phone1 = "801-654-6454";
            //customer4.Address1 = "1243 Liberty Ave.";
            //customer4.City = "Brigham";
            //customer4.State = "UT";
            //customer4.Zipcode = "84411";

            //Customer customer5 = new Customer();
            //customer5.Designation = Core.Enum.CustomerDesignationEnum.Company;
            //customer5.CompanyName = "Crystal Reach Apartments";
            //customer5.FirstName = "Amy";
            //customer5.LastName = "Richards";
            //customer5.Phone1 = "801-756-5498";
            //customer5.Address1 = "5422 Ridgeline Dr.";
            //customer5.City = "Logan";
            //customer5.State = "UT";
            //customer5.Zipcode = "84412";

            //Customer customer6 = new Customer();
            //customer6.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer6.FirstName = "Josh";
            //customer6.LastName = "Douglas";
            //customer6.Phone1 = "801-644-9164";
            //customer6.Address1 = "804 Panorama Dr";
            //customer6.City = "Ogden";
            //customer6.State = "UT";
            //customer6.Zipcode = "84403";

            //Customer customer7 = new Customer();
            //customer7.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer7.FirstName = "Jon";
            //customer7.LastName = "Douglas";
            //customer7.Phone1 = "801-644-9163";
            //customer7.Address1 = "1234 TODO Street";
            //customer7.City = "Ogden";
            //customer7.State = "UT";
            //customer7.Zipcode = "TODO";

            //Customer customer8 = new Customer();
            //customer8.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer8.FirstName = "Jim";
            //customer8.LastName = "Douglas";
            //customer8.Phone1 = "801-644-9165";
            //customer8.Address1 = "5742 S 1100 E";
            //customer8.City = "Ogden";
            //customer8.State = "UT";
            //customer8.Zipcode = "84405";

            

            context.Customers.AddOrUpdate(customer1);
            //context.Customers.AddOrUpdate(customer2);
            //context.Customers.AddOrUpdate(customer3);
            //context.Customers.AddOrUpdate(customer4);
            //context.Customers.AddOrUpdate(customer5);
            //context.Customers.AddOrUpdate(customer6);
            //context.Customers.AddOrUpdate(customer7);
            //context.Customers.AddOrUpdate(customer8);
        }

        private void AddServices(RepairReach.Data.RepairReachContext context)
        {
            Service service1 = new Service();
            service1.Name = "Inspection";
            service1.Amount = 45.00M;
            service1.CostAmount = 20.00M;

            Service service2 = new Service();
            service2.Name = "Washer Drum Installation";
            service2.Amount = 400.00M;
            service2.CostAmount = 250.00M;

            Service service3 = new Service();
            service3.Name = "Garbage Disposer Clog Repair";
            service3.Amount = 79.99M;
            service3.CostAmount = 40.00M;

            Service service4 = new Service();
            service4.Name = "Microwave Power Repair";
            service4.Amount = 59.99M;
            service4.CostAmount = 30.00M;

            Service service5 = new Service();
            service5.Name = "Refrigerator Repair";
            service5.Amount = 500.00M;
            service5.CostAmount = 300.00M;


            

            context.Services.AddOrUpdate(service1);
            context.Services.AddOrUpdate(service2);
            context.Services.AddOrUpdate(service3);
            context.Services.AddOrUpdate(service4);
            context.Services.AddOrUpdate(service5);
        }

        private void AddAppliances(RepairReach.Data.RepairReachContext context)
        {
            Appliance appliance1 = new Appliance();
            appliance1.Make = "LG";
            appliance1.Type = "Washer";
            appliance1.ModelNumber = "WM3470HVA";
            appliance1.SerialNumber = "EXAMPLE";
            appliance1.JobId = 1;
            appliance1.ProblemDescription = "Won't start.";

            //Appliance appliance2 = new Appliance();
            //appliance2.Make = "LG";
            //appliance2.Type = "Dryer";
            //appliance2.ModelNumber = "DLEX3470V";
            //appliance2.SerialNumber = "S2";
            //appliance2.JobId = 2;

            //Appliance appliance3 = new Appliance();
            //appliance3.Make = "Whirlpool ";
            //appliance3.Type = "Refrigerator";
            //appliance3.ModelNumber = "WRX735SDBM";
            //appliance3.SerialNumber = "S3";
            //appliance3.JobId = 3;

            //Appliance appliance4 = new Appliance();
            //appliance4.Make = "Kenmore ";
            //appliance4.Type = "Oven";
            //appliance4.ModelNumber = "K123X89";
            //appliance4.SerialNumber = "S98728";
            //appliance4.JobId = 4;

            //Appliance appliance5 = new Appliance();
            //appliance5.Make = "Waste King ";
            //appliance5.Type = "GarbageDisposal";
            //appliance5.ModelNumber = "WK99887";
            //appliance5.SerialNumber = "S98291";
            //appliance5.JobId = 5;

            //Appliance appliance6 = new Appliance();
            //appliance6.Make = "GE";
            //appliance6.Type = "Oven";
            //appliance6.ModelNumber = "GE1234";
            //appliance6.SerialNumber = "S322235";
            //appliance6.JobId = 6;

            //Appliance appliance7 = new Appliance();
            //appliance7.Make = "Whirlpool ";
            //appliance7.Type = "Refrigerator";
            //appliance7.ModelNumber = "WP99721";
            //appliance7.SerialNumber = "S838921";
            //appliance7.JobId = 7;

            //Appliance appliance8 = new Appliance();
            //appliance8.Make = "Kenmore ";
            //appliance8.Type = "Washer";
            //appliance8.ModelNumber = "KEN83291";
            //appliance8.SerialNumber = "S9291102";
            //appliance8.JobId = 8;

            context.Appliances.AddOrUpdate(appliance1);
            //context.Appliances.AddOrUpdate(appliance2);
            //context.Appliances.AddOrUpdate(appliance3);
            //context.Appliances.AddOrUpdate(appliance4);
            //context.Appliances.AddOrUpdate(appliance5);
            //context.Appliances.AddOrUpdate(appliance6);
            //context.Appliances.AddOrUpdate(appliance7);
            //context.Appliances.AddOrUpdate(appliance8);
        }

        private void AddRoles(RepairReach.Data.RepairReachContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            List<string> roles = new List<string>();
            string adminRole = "Admin";
            string technicianRole = "Technician";
            string dispatcherRole = "Dispatcher";
            string salesRepresentativeRole = "SalesRepresentative";
            string ownerRole = "Owner";

            

            roles.Add(adminRole);
            roles.Add(technicianRole);
            roles.Add(dispatcherRole);
            roles.Add(salesRepresentativeRole);
            roles.Add(ownerRole);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }
        }

        private void AddUsers(RepairReach.Data.RepairReachContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser u1 = new ApplicationUser();
            u1.UserName = "DEMOADMIN";
            u1.StaffId = 3;

            ApplicationUser u3 = new ApplicationUser();
            u3.UserName = "DEMOTECH";
            u3.StaffId = 1;

            ApplicationUser u4 = new ApplicationUser();
            u4.UserName = "DEMODISPATCH";
            u4.StaffId = 2;

            //ApplicationUser u5 = new ApplicationUser();
            //u5.UserName = "BOBBY";
            //u5.StaffId = 3;

            //ApplicationUser u6 = new ApplicationUser();
            //u6.UserName = "SALLY";
            //u6.StaffId = 4;

            

            //var adminResult = userManager.Create(u1, "123456");
            //var technicianResult = userManager.Create(u2, "123456");
            var u1Result = userManager.Create(u1, "123456");
            var u3Result = userManager.Create(u3, "123456");
            var u4Result = userManager.Create(u4, "123456");
            //var u5Result = userManager.Create(u5, "123456");
            //var u6Result = userManager.Create(u6, "123456");

            //if (adminResult.Succeeded) userManager.AddToRole(u1.Id, "Admin");
            //if (technicianResult.Succeeded) userManager.AddToRole(u2.Id, "Technician");
            if (u1Result.Succeeded) userManager.AddToRole(u1.Id, "Owner");
            if (u3Result.Succeeded) userManager.AddToRole(u3.Id, "Technician");
            if (u4Result.Succeeded) userManager.AddToRole(u4.Id, "Dispatcher");
            //if (u5Result.Succeeded) userManager.AddToRole(u5.Id, "SalesRepresentative");
            //if (u6Result.Succeeded) userManager.AddToRole(u6.Id, "Dispatcher");


        }

        private void AddJobs(RepairReach.Data.RepairReachContext context)
        {
            var job1 = new Job();
            job1.JobId = 1;
            job1.CustomerId = 1;
            job1.JobNumber = 1000;
            job1.JobStatusId = 3;
            job1.JobCategoryId = 2;
            job1.StaffId = 2;
            job1.Address1 = "1234 Pinacle St.";
            job1.City = "Logan";
            job1.State = "UT";
            job1.Zipcode = "84321";
            job1.ContactFirstName = "John";
            job1.ContactLastName = "Doe";
            job1.ContactPhone1 = "801-123-1234";
            job1.JobScheduled = new DateTime(2014, 5, 14).Date;
            job1.JobCreated = DateTime.UtcNow;
            job1.LastViewedBy = "DemoAdmin";
            job1.LastViewedTime = DateTime.UtcNow;
            var location1 = new Location();
            location1.lat = 0;
            location1.lng = 0;
            job1.Location = location1;

            //var job2 = new Job();
            //job2.CustomerId = 2;
            //job2.JobNumber = 1001;
            //job2.JobStatusId = 5;
            //job2.JobCategoryId = 2;
            //job2.Address1 = "321 Jefferson Ave.";
            //job2.City = "Brigham";
            //job2.State = "UT";
            //job2.Zipcode = "84411";
            //job2.ContactFirstName = "Doug";
            //job2.ContactLastName = "Hanson";
            //job2.ContactPhone1 = "801-123-1111";
            //job2.Description = "Dryer will not turn on.";
            //job2.CollectPaymentOnSite = true;
            //job2.JobScheduled = new DateTime(2014, 5, 11).Date;
            //job2.JobCreated = DateTime.Today;
            //job2.StaffId = 3;
            //job2.LastViewedBy = "Sauron";
            //job2.LastViewedTime = DateTime.UtcNow;

            //var job3 = new Job();
            //job3.CustomerId = 3;
            //job3.JobNumber = 1002;
            //job3.JobStatusId = 4;
            //job3.JobCategoryId = 3;
            //job3.Address1 = "5677 Madison St.";
            //job3.City = "Logan";
            //job3.State = "UT";
            //job3.Zipcode = "84412";
            //job3.ContactFirstName = "Stan";
            //job3.ContactLastName = "Stanleyson";
            //job3.ContactPhone1 = "801-321-2323";
            //job3.Description = "Water is leaking inside the refrigerator.";
            //job3.CollectPaymentOnSite = true;
            //job3.JobScheduled = new DateTime(2014, 5, 12).Date;
            //job3.JobCreated = DateTime.Today;
            //job3.StaffId = 3;
            //job3.LastViewedBy = "An Oracle";
            //job3.LastViewedTime = DateTime.UtcNow;

            //var job4 = new Job();
            //job4.CustomerId = 4;
            //job4.JobNumber = 1003;
            //job4.JobStatusId = 3;
            //job4.JobCategoryId = 2;
            //job4.Address1 = "1243 Liberty Ave.";
            //job4.City = "Brigham";
            //job4.State = "UT";
            //job4.Zipcode = "84411";
            //job4.ContactFirstName = "Jenny";
            //job4.ContactLastName = "Robinson";
            //job4.ContactPhone1 = "801-654-6454";
            //job4.Description = "Burners on oven are not working";
            //job4.CollectPaymentOnSite = true;
            //job4.JobScheduled = new DateTime(2014, 5, 15).Date;
            //job4.JobCreated = DateTime.Today;
            //job4.StaffId = 3;
            //job4.LastViewedBy = "Bruce";
            //job4.LastViewedTime = DateTime.UtcNow;

            //var job5 = new Job();
            //job5.CustomerId = 5;
            //job5.JobNumber = 1004;
            //job5.JobStatusId = 10;
            //job5.JobCategoryId = 3;
            //job5.Address1 = "5422 Ridgeline Dr.";
            //job5.Address2 = "APT E201";
            //job5.City = "Logan";
            //job5.State = "UT";
            //job5.Zipcode = "84412";
            //job5.ContactFirstName = "Amy";
            //job5.ContactLastName = "Richards";
            //job5.ContactPhone1 = "801-756-5498";
            //job5.Description = "Garbage disposal not working.";
            //job5.CollectPaymentOnSite = true;
            //job5.JobScheduled = new DateTime(2014, 5, 9).Date;
            //job5.JobClosed = new DateTime(2014, 5, 12).Date;
            //job5.JobCreated = DateTime.Today;
            //job5.StaffId = 3;
            //job5.LastViewedBy = "Al Gore";
            //job5.LastViewedTime = DateTime.UtcNow;

            //var job6 = new Job();
            //job6.CustomerId = 5;
            //job6.JobNumber = 1005;
            //job6.JobStatusId = 10;
            //job6.JobCategoryId = 3;
            //job6.Address1 = "804 Panorama Dr";
            //job6.City = "Ogden";
            //job6.State = "UT";
            //job6.Zipcode = "84403";
            //job6.ContactFirstName = "Josh";
            //job6.ContactLastName = "Douglas";
            //job6.ContactPhone1 = "801-644-9164";
            //job6.Description = "Oven not working.";
            //job6.CollectPaymentOnSite = true;
            //job6.JobScheduled = new DateTime(2014, 5, 9).Date;
            //job6.JobClosed = new DateTime(2014, 5, 12).Date;
            //job6.JobCreated = DateTime.Today;
            //job6.StaffId = 3;
            //job6.LastViewedBy = "Ted Bundy";
            //job6.LastViewedTime = DateTime.UtcNow;

            //var job7 = new Job();
            //job7.CustomerId = 5;
            //job7.JobNumber = 1004;
            //job7.JobStatusId = 10;
            //job7.JobCategoryId = 3;
            //job7.Address1 = "1234 TODO Street";
            //job7.City = "Ogden";
            //job7.State = "UT";
            //job7.Zipcode = "TODO";
            //job7.ContactFirstName = "Jon";
            //job7.ContactLastName = "Douglas";
            //job7.ContactPhone1 = "801-644-9163";
            //job7.Description = "Refrigerator not working.";
            //job7.CollectPaymentOnSite = true;
            //job7.JobScheduled = new DateTime(2014, 5, 9).Date;
            //job7.JobClosed = new DateTime(2014, 5, 12).Date;
            //job7.JobCreated = DateTime.Today;
            //job7.StaffId = 3;
            //job7.LastViewedBy = "Ted Bundy";
            //job7.LastViewedTime = DateTime.UtcNow;

            //var job8 = new Job();
            //job8.CustomerId = 5;
            //job8.JobNumber = 1004;
            //job8.JobStatusId = 10;
            //job8.JobCategoryId = 3;
            //job8.Address1 = "5742 S 1100 E";
            //job8.Address2 = "APT E201";
            //job8.City = "Ogden";
            //job8.State = "UT";
            //job8.Zipcode = "84405";
            //job8.ContactFirstName = "Jim";
            //job8.ContactLastName = "Douglas";
            //job8.ContactPhone1 = "801-644-9165";
            //job8.Description = "Washer not working.";
            //job8.CollectPaymentOnSite = true;
            //job8.JobScheduled = new DateTime(2014, 5, 9).Date;
            //job8.JobClosed = new DateTime(2014, 5, 12).Date;
            //job8.JobCreated = DateTime.Today;
            //job8.StaffId = 3;
            //job8.LastViewedBy = "Ted Bundy";
            //job8.LastViewedTime = DateTime.UtcNow;

            

            context.Jobs.AddOrUpdate(job1);
            //context.Jobs.AddOrUpdate(job2);
            //context.Jobs.AddOrUpdate(job3);
            //context.Jobs.AddOrUpdate(job4);
            //context.Jobs.AddOrUpdate(job5);
            //context.Jobs.AddOrUpdate(job6);
            //context.Jobs.AddOrUpdate(job7);
            //context.Jobs.AddOrUpdate(job8);
        }

        //TODO: Line items after refactor

        private void AddJobNotes(RepairReach.Data.RepairReachContext context)
        {
            var jobNote1 = new JobNote();
            jobNote1.JobId = 1;
            jobNote1.Note = "Customer would like to keep any old parts.";
            jobNote1.CreatedBy = "BOBBY";
            jobNote1.CreatedDate = new DateTime(2014, 5, 14).Date;

            var jobNote2 = new JobNote();
            jobNote1.JobId = 2;
            jobNote1.Note = "House is behind the long driveway.";
            jobNote1.CreatedBy = "SALLY";
            jobNote1.CreatedDate = new DateTime(2014, 5, 13).Date;

            var jobNote3 = new JobNote();
            jobNote1.JobId = 5;
            jobNote1.Note = "Customer will PIF on friday";
            jobNote1.CreatedBy = "BOBBY";
            jobNote1.CreatedDate = new DateTime(2014, 5, 13).Date;

            context.JobNotes.AddOrUpdate(jobNote1);
            context.JobNotes.AddOrUpdate(jobNote2);
            context.JobNotes.AddOrUpdate(jobNote3);
        }

        private void AddPayments(RepairReach.Data.RepairReachContext context)
        {
            var payment1 = new Payment();
            payment1.JobId = 5;
            payment1.PaymentMethodId = 1;
            payment1.PaymentAmount = 150.00M;
            payment1.DatePaid = new DateTime(2014, 5, 12).Date;
            payment1.EnteredBy = "JON";

        }

        private void AddAppointments(RepairReachContext context)
        {
            var appointment1 = new Appointment();
            appointment1.JobId = 1;
            appointment1.TechnicianStaffId = 1;
            appointment1.Note = "This is an example note for the appointment.";
            appointment1.Created = DateTime.UtcNow;
            appointment1.CreatedBy = "DemoAdmin";
            appointment1.IsCompleted = false;

            //for when it is seeded - 8AM to 10AM
            appointment1.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            appointment1.EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);

            //convert to UTC
            var company = context.Companies.First();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            appointment1.StartTime = DateTime.SpecifyKind(appointment1.StartTime, DateTimeKind.Unspecified);
            appointment1.EndTime = DateTime.SpecifyKind(appointment1.EndTime, DateTimeKind.Unspecified);
            appointment1.StartTime = TimeZoneInfo.ConvertTimeToUtc(appointment1.StartTime, timeZoneInfo);
            appointment1.EndTime = TimeZoneInfo.ConvertTimeToUtc(appointment1.EndTime, timeZoneInfo);

            context.Appointments.AddOrUpdate(appointment1);
        }

        private void AddQuickLineItems(RepairReachContext context)
        {
            var quickLine1 = new QuickLineItem();
            quickLine1.Description = "Trip Charge";
            quickLine1.ServiceName = "Trip Charge";
            quickLine1.ServiceQty = 1;
            quickLine1.ServiceEach = 29.99M;
            quickLine1.ServiceCost = 19.99M;

            var quickLine2 = new QuickLineItem();
            quickLine2.Description = "Estimate Fee";
            quickLine2.ServiceName = "Estimate Fee";
            quickLine2.ServiceQty = 1;
            quickLine2.ServiceEach = 30.00M;
            quickLine2.ServiceCost = 15.99M;

            var quickLine3 = new QuickLineItem();
            quickLine3.Description = "Drive Coupling";
            quickLine3.PartName = "Drive Coupling";
            quickLine3.PartQty = 1;
            quickLine3.PartEach = 12.00M;
            quickLine3.PartCost = 4.00M;
            quickLine3.PartNumber = "892713";

            var quickLine4 = new QuickLineItem();
            quickLine4.Description = "Washer Hose Installation";
            quickLine4.ServiceName = "Washer Hose Installation";
            quickLine4.ServiceQty = 1;
            quickLine4.ServiceEach = 49.99M;
            quickLine4.ServiceCost = 29.99M;
            quickLine4.PartName = "WM3470HVA Hose";
            quickLine4.PartQty = 1;
            quickLine4.PartEach = 29.99M;
            quickLine4.PartCost = 19.99M;
            quickLine4.PartNumber = "820117";

            context.QuickLineItems.AddOrUpdate(quickLine1);
            context.QuickLineItems.AddOrUpdate(quickLine2);
            context.QuickLineItems.AddOrUpdate(quickLine3);
            context.QuickLineItems.AddOrUpdate(quickLine4);
        }

        private void AddVendors(RepairReachContext context)
        {
            var vendor1 = new Vendor();
            vendor1.CompanyName = "Example Parts Company";
            vendor1.CompanyPhone = "(801) 123-1234";
            vendor1.CompanyEmail = "person@examplecompany.com";
            vendor1.Address1 = "1234 Example St.";
            vendor1.City = "Logan";
            vendor1.State = "UT";
            vendor1.ZipCode = "84321";
            vendor1.Contact1Name = "Person";
            vendor1.Contact1Title = "Owner";
            vendor1.Contact1Phone = "(801) 123-1234";
            vendor1.Contact1Email = "person@examplecompany.com";

            context.Vendors.AddOrUpdate(vendor1);
        }

        private void AddLineItems(RepairReachContext context)
        {
            var lineItem1 = new LineItem();
            lineItem1.JobId = 1;
            lineItem1.LineItemNumber = 1;
            lineItem1.StaffId = 1;
            lineItem1.TaxRateId = 2;
            lineItem1.Description = "Trip Charge";
            lineItem1.ServiceName = "Trip Charge";
            lineItem1.ServiceQty = 1;
            lineItem1.ServiceEach = 29.99M;
            lineItem1.ServiceCost = 19.99M;

            var lineItem2 = new LineItem();
            lineItem2.JobId = 1;
            lineItem2.LineItemNumber = 2;
            lineItem2.StaffId = 1;
            lineItem2.TaxRateId = 2;
            lineItem2.Description = "Estimate Fee";
            lineItem2.ServiceName = "Estimate Fee";
            lineItem2.ServiceQty = 1;
            lineItem2.ServiceEach = 30.00M;
            lineItem2.ServiceCost = 15.99M;

            var lineItem3 = new LineItem();
            lineItem3.JobId = 1;
            lineItem3.LineItemNumber = 3;
            lineItem3.StaffId = 1;
            lineItem3.TaxRateId = 2;
            lineItem3.Description = "Washer Hose Installation";
            lineItem3.ServiceName = "Washer Hose Installation";
            lineItem3.ServiceQty = 1;
            lineItem3.ServiceEach = 49.99M;
            lineItem3.ServiceCost = 29.99M;
            lineItem3.PartName = "WM3470HVA Hose";
            lineItem3.PartQty = 1;
            lineItem3.PartEach = 29.99M;
            lineItem3.PartCost = 19.99M;
            lineItem3.PartNumber = "820117";


            context.LineItems.AddOrUpdate(lineItem1);
            context.LineItems.AddOrUpdate(lineItem2);
            context.LineItems.AddOrUpdate(lineItem3);
        }

        private void AddHowDidYouFindUses(RepairReachContext context)
        {
            HowDidYouFindUs h1 = new HowDidYouFindUs();
            h1.SequenceNumber = 1;
            h1.Description = "Online";

            HowDidYouFindUs h2 = new HowDidYouFindUs();
            h2.SequenceNumber = 2;
            h2.Description = "Referral";

            HowDidYouFindUs h3 = new HowDidYouFindUs();
            h3.SequenceNumber = 3;
            h3.Description = "Past Customer";

            context.HowDidYouFindUses.AddOrUpdate(h1);
            context.HowDidYouFindUses.AddOrUpdate(h2);
            context.HowDidYouFindUses.AddOrUpdate(h3);
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}

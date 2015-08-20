using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Web.Script.Serialization;
using BusinessTracker.Core.Model;
using BusinessTracker.Data.Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessTracker.Data.DataContexts.CoreMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BusinessTracker.Data.BusinessTrackerContext>
    {
        public Configuration()
        {
            //2014.01.07 - JDD
            //to update the database this needs to be true, but when testing this needs to be false. need to do some further research later on.
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BusinessTracker.Data.BusinessTrackerContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            AddCompany(context);
            AddTaxRates(context);
            AddCategories(context);
            AddStatuses(context);
            AddStaff(context);
            AddCustomers(context);
            AddServices(context);
            AddRoles(context);
            AddUsers(context);
            AddAppliances(context);
        }

        private void AddCompany(BusinessTracker.Data.BusinessTrackerContext context)
        {
            Company company1 = new Company();
            company1.Name = "Test Company";
            company1.Phone = "(801) 123-1234";
            company1.Address1 = "1234 Main St.";
            company1.City = "Logan";
            company1.State = "UT";
            company1.Zipcode = "84412";
            company1.Logo = 2;

            context.Companies.AddOrUpdate(company1);
        }

        private void AddTaxRates(BusinessTracker.Data.BusinessTrackerContext context)
        {
            TaxRate taxRate1 = new TaxRate();
            taxRate1.Name = "Ogden Sales Tax";
            taxRate1.Amount = 6.85M;
            taxRate1.IsDefaultRate = true;

            TaxRate taxRate2 = new TaxRate();
            taxRate2.Name = "Logan Sales Tax";
            taxRate2.Amount = 6.60M;
            taxRate2.IsDefaultRate = false;

            context.TaxRates.AddOrUpdate(taxRate1);
            context.TaxRates.AddOrUpdate(taxRate2);
        }

        private void AddCategories(BusinessTracker.Data.BusinessTrackerContext context)
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

        private void AddStatuses(BusinessTracker.Data.BusinessTrackerContext context)
        {
            JobStatus status1 = new JobStatus();
            status1.SequenceNumber = 1;
            status1.Description = "In Hold";

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
            status7.Description = "Rescheduled";

            JobStatus status8 = new JobStatus();
            status8.SequenceNumber = 8;
            status8.Description = "Mail Invoice";

            JobStatus status9 = new JobStatus();
            status9.SequenceNumber = 9;
            status9.Description = "Awaiting Payment";

            JobStatus status10 = new JobStatus();
            status10.SequenceNumber = 10;
            status10.Description = "Closed";

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
        }

        private void AddStaff(BusinessTracker.Data.BusinessTrackerContext context)
        {
            Staff staff1 = new Staff();
            staff1.DisplayName = "JOSH";
            staff1.FirstName = "Josh";
            staff1.LastName = "Test";
            staff1.UserTitle = Core.Enum.UserTitleEnum.Owner;

            Staff staff2 = new Staff();
            staff2.DisplayName = "JON";
            staff2.FirstName = "Jon";
            staff2.LastName = "Test";
            staff2.UserTitle = Core.Enum.UserTitleEnum.Technician;

            Staff staff3 = new Staff();
            staff3.DisplayName = "BOBBY";
            staff3.FirstName = "Bobby";
            staff3.LastName = "Test";
            staff3.UserTitle = Core.Enum.UserTitleEnum.SalesRepresentative;

            Staff staff4 = new Staff();
            staff4.DisplayName = "SALLY";
            staff4.FirstName = "Sally";
            staff4.LastName = "Test";
            staff4.UserTitle = Core.Enum.UserTitleEnum.Dispatcher;

            context.Staff.AddOrUpdate(staff1);
            context.Staff.AddOrUpdate(staff2);
            context.Staff.AddOrUpdate(staff3);
            context.Staff.AddOrUpdate(staff4);
        }

        private void AddCustomers(BusinessTracker.Data.BusinessTrackerContext context)
        {
            Customer customer1 = new Customer();
            customer1.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer1.CompanyName = " ";
            customer1.FirstName = "Jim";
            customer1.LastName = "Thore";
            customer1.Phone1 = "801-123-3214";
            customer1.Address1 = "1234 Pinacle St.";
            customer1.City = "Logan";
            customer1.State = "UT";
            customer1.Zipcode = "84412";

            Customer customer2 = new Customer();
            customer2.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer2.CompanyName = " ";
            customer2.FirstName = "Doug";
            customer2.LastName = "Hanson";
            customer2.Phone1 = "801-123-1111";
            customer2.Address1 = "321 Jefferson Ave.";
            customer2.City = "Brigham";
            customer2.State = "UT";
            customer2.Zipcode = "84411";

            Customer customer3 = new Customer();
            customer3.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer3.CompanyName = " ";
            customer3.FirstName = "Stan";
            customer3.LastName = "Stanleyson";
            customer3.Phone1 = "801-321-2323";
            customer3.Address1 = "5677 Madison St.";
            customer3.City = "Logan";
            customer3.State = "UT";
            customer3.Zipcode = "84412";

            Customer customer4 = new Customer();
            customer4.Designation = Core.Enum.CustomerDesignationEnum.Individual;
            //customer4.CompanyName = " ";
            customer4.FirstName = "Jenny";
            customer4.LastName = "Robinson";
            customer4.Phone1 = "801-654-6454";
            customer4.Address1 = "1243 Liberty Ave.";
            customer4.City = "Brigham";
            customer4.State = "UT";
            customer4.Zipcode = "84411";

            Customer customer5 = new Customer();
            customer5.Designation = Core.Enum.CustomerDesignationEnum.Company;
            customer5.CompanyName = "Crystal Reach Apartments";
            customer5.FirstName = "Amy";
            customer5.LastName = "Richards";
            customer5.Phone1 = "801-756-5498";
            customer5.Address1 = "5422 Ridgeline Dr.";
            customer5.City = "Logan";
            customer5.State = "UT";
            customer5.Zipcode = "84412";

            context.Customers.AddOrUpdate(customer1);
            context.Customers.AddOrUpdate(customer2);
            context.Customers.AddOrUpdate(customer3);
            context.Customers.AddOrUpdate(customer4);
            context.Customers.AddOrUpdate(customer5);
        }

        private void AddServices(BusinessTracker.Data.BusinessTrackerContext context)
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

        private void AddAppliances(BusinessTracker.Data.BusinessTrackerContext context)
        {
            Appliance appliance1 = new Appliance();
            appliance1.Make = "LG";
            appliance1.Type = "Washer";
            appliance1.ModelNumber = "WM3470HVA";
            appliance1.SerialNumber = "S1";
            appliance1.CustomerId = 1;

            Appliance appliance2 = new Appliance();
            appliance2.Make = "LG";
            appliance2.Type = "Dryer";
            appliance2.ModelNumber = "DLEX3470V";
            appliance2.SerialNumber = "S2";
            appliance2.CustomerId = 1;

            Appliance appliance3 = new Appliance();
            appliance3.Make = "Whirlpool ";
            appliance3.Type = "Refrigerator";
            appliance3.ModelNumber = "WRX735SDBM";
            appliance3.SerialNumber = "S3";
            appliance3.CustomerId = 1;

            context.Appliances.AddOrUpdate(appliance1);
            context.Appliances.AddOrUpdate(appliance2);
            context.Appliances.AddOrUpdate(appliance3);
        }

        private void AddRoles(BusinessTracker.Data.BusinessTrackerContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            List<string> roles = new List<string>();
            string adminRole = "Admin";
            string technicianRole = "Technician";
            string dispatcherRole = "Dispatcher";
            string salesRepresentativeRole = "Sales Representative";

            roles.Add(adminRole);
            roles.Add(technicianRole);
            roles.Add(dispatcherRole);
            roles.Add(salesRepresentativeRole);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }
        }

        private void AddUsers(BusinessTracker.Data.BusinessTrackerContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser u1 = new ApplicationUser();
            u1.UserName = "DemoAdmin";
            u1.StaffId = 1;

            ApplicationUser u2 = new ApplicationUser();
            u2.UserName = "DemoTechnician";
            u2.StaffId = 2;
            
            var adminResult = userManager.Create(u1, "123456");
            var technicianResult = userManager.Create(u2, "123456");

            if(adminResult.Succeeded)
            userManager.AddToRole(u1.Id, "Admin");

            if(technicianResult.Succeeded)
            userManager.AddToRole(u2.Id, "Technician");


        }
    }
}

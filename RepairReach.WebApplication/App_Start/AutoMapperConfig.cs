using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication
{
    public class AutoMapperConfig
    {
        public static void Initialise()
        {
            MapQuickLineItem();
            MapVendor();
            MapJobStatus();
            MapJobCategory();
            MapHowDidYouFindUs();
            MapTaxRate();
            MapCompany();
            MapCustomer();
            MapJob();
            MapAppliance();
            MapAppointment();
            MapJobNote();
            MapLineItem();
            MapPayment();
            MapActivityEvent();
            MapStaff();
            MapPaymentMethod();
            Mapper.AssertConfigurationIsValid();
        }

        private static void MapQuickLineItem()
        {
            Mapper.CreateMap<QuickLineItem, QuickLineItemIndexViewModel>()
                .ForSourceMember(x => x.PartName, y => y.Ignore())
                .ForSourceMember(x => x.PartQty, y => y.Ignore())
                .ForSourceMember(x => x.PartEach, y => y.Ignore())
                .ForSourceMember(x => x.PartCost, y => y.Ignore())
                .ForSourceMember(x => x.PartNumber, y => y.Ignore())
                .ForSourceMember(x => x.ServiceName, y => y.Ignore())
                .ForSourceMember(x => x.ServiceQty, y => y.Ignore())
                .ForSourceMember(x => x.ServiceEach, y => y.Ignore())
                .ForSourceMember(x => x.ServiceCost, y => y.Ignore());
            Mapper.CreateMap<QuickLineItem, QuickLineItemCreateViewModel>();
            Mapper.CreateMap<QuickLineItem, QuickLineItemEditViewModel>();

            Mapper.CreateMap<QuickLineItemCreateViewModel, QuickLineItem>();
            Mapper.CreateMap<QuickLineItemEditViewModel, QuickLineItem>();
        }

        private static void MapVendor()
        {
            Mapper.CreateMap<Vendor, VendorIndexViewModel>();
            Mapper.CreateMap<Vendor, VendorCreateViewModel>();
            Mapper.CreateMap<Vendor, VendorEditViewModel>();

            Mapper.CreateMap<VendorCreateViewModel, Vendor>();
            Mapper.CreateMap<VendorEditViewModel, Vendor>();
        }

        private static void MapJobStatus()
        {
            Mapper.CreateMap<JobStatus, JobStatusIndexViewModel>();
            Mapper.CreateMap<JobStatus, JobStatusCreateViewModel>();

            Mapper.CreateMap<JobStatusIndexViewModel, JobStatus>()
                .ForMember(x => x.Jobs, y => y.Ignore());
            Mapper.CreateMap<JobStatusCreateViewModel, JobStatus>()
                .ForMember(x => x.Jobs, y => y.Ignore())
                .ForMember(x => x.JobStatusId, y => y.Ignore());
        }

        private static void MapJobCategory()
        {
            Mapper.CreateMap<JobCategory, JobCategoryIndexViewModel>();
            Mapper.CreateMap<JobCategory, JobCategoryCreateViewModel>();

            Mapper.CreateMap<JobCategoryIndexViewModel, JobCategory>()
                .ForMember(x => x.Jobs, y => y.Ignore());
            Mapper.CreateMap<JobCategoryCreateViewModel, JobCategory>()
                .ForMember(x => x.Jobs, y => y.Ignore())
                .ForMember(x => x.JobCategoryId, y => y.Ignore());
        }

        private static void MapHowDidYouFindUs()
        {
            Mapper.CreateMap<HowDidYouFindUs, HowDidYouFindUsIndexViewModel>();
            Mapper.CreateMap<HowDidYouFindUs, HowDidYouFindUsCreateViewModel>();

            Mapper.CreateMap<HowDidYouFindUsIndexViewModel, HowDidYouFindUs>()
                .ForMember(x => x.Customers, y => y.Ignore());
            Mapper.CreateMap<HowDidYouFindUsCreateViewModel, HowDidYouFindUs>()
                .ForMember(x => x.Customers, y => y.Ignore())
                .ForMember(x => x.HowDidYouFindUsId, y => y.Ignore());
        }

        private static void MapTaxRate()
        {
            Mapper.CreateMap<TaxRate, TaxRateIndexViewModel>();
            Mapper.CreateMap<TaxRate, TaxRateCreateViewModel>();
            Mapper.CreateMap<TaxRate, TaxRateEditViewModel>();

            Mapper.CreateMap<TaxRateCreateViewModel, TaxRate>()
                .ForMember(x => x.TaxRateId, y => y.Ignore())
                .ForMember(x => x.LineItems, y => y.Ignore());
            Mapper.CreateMap<TaxRateEditViewModel, TaxRate>()
                .ForMember(x => x.LineItems, y => y.Ignore());
        }

        private static void MapCompany()
        {
            Mapper.CreateMap<Company, CompanyIndexViewModel>();
            Mapper.CreateMap<Company, CompanyCreateViewModel>();
            Mapper.CreateMap<Company, CompanyEditViewModel>();

            Mapper.CreateMap<CompanyCreateViewModel, Company>();
            Mapper.CreateMap<CompanyEditViewModel, Company>();
        }

        private static void MapCustomer()
        {
            Mapper.CreateMap<Customer, CustomerIndexViewModel>();
            Mapper.CreateMap<Customer, CustomerCreateViewModel>();
            Mapper.CreateMap<Customer, CustomerEditViewModel>();

            Mapper.CreateMap<CustomerCreateViewModel, Customer>()
                .ForMember(x => x.CustomerId, y => y.Ignore())
                .ForMember(x => x.ImportedCustomerId, y => y.Ignore())
                .ForMember(x => x.HowDidYouFindUs, y => y.Ignore())
                .ForMember(x => x.Jobs, y => y.Ignore());
            Mapper.CreateMap<CustomerEditViewModel, Customer>()
                .ForMember(x => x.ImportedCustomerId, y => y.Ignore())
                .ForMember(x => x.HowDidYouFindUs, y => y.Ignore())
                .ForMember(x => x.Jobs, y => y.Ignore());
        }

        private static void MapJob()
        {
            Mapper.CreateMap<Job, JobIndexViewModel>()
                .ForMember(x => x.CategoryDescription, y => y.MapFrom(j => j.JobCategory.Description))
                .ForMember(x => x.StatusDescription, y => y.MapFrom(j => j.JobStatus.Description))
                .ForMember(x => x.SalesRepDisplayName, y => y.MapFrom(j => j.SalesRepresentative.DisplayName))
                .ForMember(x => x.TotalAmount, y => y.MapFrom(j => j.GrandTotal));
            Mapper.CreateMap<Job, JobCreateViewModel>()
                .ForMember(x => x.CustomerDisplayName, y => y.MapFrom(j => j.Customer.DisplayName));
            Mapper.CreateMap<Job, JobEditViewModel>();

            Mapper.CreateMap<JobCreateViewModel, Job>()
                .ForMember(x => x.JobId, y => y.Ignore())
                .ForMember(x => x.Customer, y => y.Ignore())
                .ForMember(x => x.JobStatus, y => y.Ignore())
                .ForMember(x => x.JobCategory, y => y.Ignore())
                .ForMember(x => x.JobNotes, y => y.Ignore())
                .ForMember(x => x.Location, y => y.Ignore())
                .ForMember(x => x.LastViewedTime, y => y.Ignore())
                .ForMember(x => x.LastViewedBy, y => y.Ignore())
                .ForMember(x => x.JobCreated, y => y.Ignore())
                .ForMember(x => x.JobAuthorized, y => y.Ignore())
                .ForMember(x => x.JobScheduled, y => y.Ignore())
                .ForMember(x => x.JobStarted, y => y.Ignore())
                .ForMember(x => x.JobFinished, y => y.Ignore())
                .ForMember(x => x.JobClosed, y => y.Ignore())
                .ForMember(x => x.JobBilled, y => y.Ignore())
                .ForMember(x => x.IsAuthorized, y => y.Ignore())
                .ForMember(x => x.Payments, y => y.Ignore())
                .ForMember(x => x.LineItems, y => y.Ignore())
                .ForMember(x => x.Appointments, y => y.Ignore())
                .ForMember(x => x.ActivityEvents, y => y.Ignore())
                .ForMember(x => x.Appliances, y => y.Ignore())
                .ForMember(x => x.SalesRepresentative, y => y.Ignore())
                .ForMember(x => x.ImportedJobId, y => y.Ignore());
            Mapper.CreateMap<JobEditViewModel, Job>()
                .ForMember(x => x.Customer, y => y.Ignore())
                .ForMember(x => x.JobStatus, y => y.Ignore())
                .ForMember(x => x.JobCategory, y => y.Ignore())
                .ForMember(x => x.JobNotes, y => y.Ignore())
                .ForMember(x => x.Location, y => y.Ignore())
                .ForMember(x => x.JobAuthorized, y => y.Ignore())
                .ForMember(x => x.JobScheduled, y => y.Ignore())
                .ForMember(x => x.JobStarted, y => y.Ignore())
                .ForMember(x => x.JobFinished, y => y.Ignore())
                .ForMember(x => x.JobBilled, y => y.Ignore())
                .ForMember(x => x.IsAuthorized, y => y.Ignore())
                .ForMember(x => x.Payments, y => y.Ignore())
                .ForMember(x => x.LineItems, y => y.Ignore())
                .ForMember(x => x.Appointments, y => y.Ignore())
                .ForMember(x => x.ActivityEvents, y => y.Ignore())
                .ForMember(x => x.Appliances, y => y.Ignore())
                .ForMember(x => x.SalesRepresentative, y => y.Ignore())
                .ForMember(x => x.ImportedJobId, y => y.Ignore());
        }

        private static void MapAppliance()
        {
            Mapper.CreateMap<Appliance, ApplianceIndexViewModel>();
            Mapper.CreateMap<Appliance, ApplianceCreateViewModel>();
            Mapper.CreateMap<Appliance, ApplianceEditViewModel>();

            Mapper.CreateMap<ApplianceCreateViewModel, Appliance>()
                .ForMember(x => x.ApplianceId, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore());
            Mapper.CreateMap<ApplianceEditViewModel, Appliance>()
                .ForMember(x => x.Job, y => y.Ignore());
        }

        private static void MapAppointment()
        {
            Mapper.CreateMap<Appointment, AppointmentIndexViewModel>();
            Mapper.CreateMap<Appointment, AppointmentCreateViewModel>()
                .ForMember(x => x.StartDate, y => y.MapFrom(a => a.StartTime))
                .ForMember(x => x.StartTime, y => y.MapFrom(a => a.StartTime))
                .ForMember(x => x.EndDate, y => y.MapFrom(a => a.EndTime))
                .ForMember(x => x.EndTime, y => y.MapFrom(a => a.EndTime))
                .ForMember(x => x.Technicians, y => y.Ignore())
                .ForMember(x => x.Map, y => y.Ignore());
            Mapper.CreateMap<Appointment, AppointmentEditViewModel>()
                .ForMember(x => x.StartDate, y => y.MapFrom(a => a.StartTime))
                .ForMember(x => x.StartTime, y => y.MapFrom(a => a.StartTime))
                .ForMember(x => x.EndDate, y => y.MapFrom(a => a.EndTime))
                .ForMember(x => x.EndTime, y => y.MapFrom(a => a.EndTime))
                .ForMember(x => x.Technicians, y => y.Ignore())
                .ForMember(x => x.Map, y => y.Ignore());
            Mapper.CreateMap<AppointmentCreateViewModel, Appointment>()
                .ForMember(x => x.AppointmentId, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore())
                .ForMember(x => x.Technician, y => y.Ignore())
                .ForMember(x => x.CreatedBy, y => y.Ignore())
                .ForMember(x => x.Created, y => y.Ignore())
                .ForMember(x => x.IsCompleted, y => y.Ignore())
                .ForMember(x => x.CompletedTime, y => y.Ignore());
            Mapper.CreateMap<AppointmentEditViewModel, Appointment>()
                .ForMember(x => x.Job, y => y.Ignore())
                .ForMember(x => x.Technician, y => y.Ignore())
                .ForMember(x => x.IsCompleted, y => y.Ignore())
                .ForMember(x => x.CompletedTime, y => y.Ignore());

        }

        private static void MapJobNote()
        {
            Mapper.CreateMap<JobNote, JobNoteIndexViewModel>();
            Mapper.CreateMap<JobNote, JobNoteCreateViewModel>();
            Mapper.CreateMap<JobNote, JobNoteEditViewModel>();

            Mapper.CreateMap<JobNoteCreateViewModel, JobNote>()
                .ForMember(x => x.JobNoteId, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore())
                .ForMember(x => x.CreatedBy, y => y.Ignore())
                .ForMember(x => x.CreatedDate, y => y.Ignore());
            Mapper.CreateMap<JobNoteEditViewModel, JobNote>()
                .ForMember(x => x.Job, y => y.Ignore());
        }

        private static void MapLineItem()
        {
            Mapper.CreateMap<LineItem, LineItemIndexViewModel>();
            Mapper.CreateMap<LineItem, LineItemCreateViewModel>();
            Mapper.CreateMap<LineItem, LineItemEditViewModel>();

            Mapper.CreateMap<LineItemCreateViewModel, LineItem>()
                .ForMember(x => x.LineItemId, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore())
                .ForMember(x => x.Technician, y => y.Ignore())
                .ForMember(x => x.TaxRate, y => y.Ignore());
            Mapper.CreateMap<LineItemEditViewModel, LineItem>()
                .ForMember(x => x.Job, y => y.Ignore())
                .ForMember(x => x.Technician, y => y.Ignore())
                .ForMember(x => x.TaxRate, y => y.Ignore());
        }

        private static void MapPayment()
        {
            Mapper.CreateMap<Payment, PaymentIndexViewModel>()
                .ForMember(x => x.PaymentMethod, y => y.MapFrom(p => p.PaymentMethod.Description));
            Mapper.CreateMap<Payment, PaymentCreateViewModel>();
            Mapper.CreateMap<Payment, PaymentEditViewModel>();

            Mapper.CreateMap<PaymentCreateViewModel, Payment>()
                .ForMember(x => x.PaymentMethod, y => y.Ignore())
                .ForMember(x => x.PaymentId, y => y.Ignore())
                .ForMember(x => x.EnteredBy, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore());
            Mapper.CreateMap<PaymentEditViewModel, Payment>()
                .ForMember(x => x.PaymentMethod, y => y.Ignore())
                .ForMember(x => x.Job, y => y.Ignore());
        }

        private static void MapActivityEvent()
        {
            Mapper.CreateMap<ActivityEvent, ManagementActivityLogViewModel>()
                .ForMember(x => x.JobNumber, y => y.MapFrom(a => a.Job.JobNumber));
            Mapper.CreateMap<ActivityEvent, JobEditActivityEventViewModel>();
        }

        private static void MapStaff()
        {
            Mapper.CreateMap<Staff, TeamIndexViewModel>();
            Mapper.CreateMap<Staff, TeamCreateViewModel>()
                .ForMember(x => x.Password, y => y.Ignore());
            Mapper.CreateMap<Staff, TeamEditViewModel>()
                .ForMember(x => x.Password, y => y.Ignore());

            Mapper.CreateMap<TeamCreateViewModel, Staff>()
                .ForMember(x => x.StaffId, y => y.Ignore())
                .ForMember(x => x.IsActive, y => y.Ignore())
                .ForMember(x => x.ImportedStaffId, y => y.Ignore())
                .ForMember(x => x.Jobs, y => y.Ignore())
                .ForMember(x => x.LineItems, y => y.Ignore())
                .ForMember(x => x.TimeClockEntries, y => y.Ignore())
                .ForMember(x => x.Appointments, y => y.Ignore());
            Mapper.CreateMap<TeamEditViewModel, Staff>()
                .ForMember(x => x.ImportedStaffId, y => y.Ignore())
                .ForMember(x => x.Jobs, y => y.Ignore())
                .ForMember(x => x.LineItems, y => y.Ignore())
                .ForMember(x => x.TimeClockEntries, y => y.Ignore())
                .ForMember(x => x.Appointments, y => y.Ignore());
        }

        private static void MapPaymentMethod()
        {
            Mapper.CreateMap<PaymentMethod, PaymentMethodIndexViewModel>();
            Mapper.CreateMap<PaymentMethod, PaymentMethodCreateViewModel>();

            Mapper.CreateMap<PaymentMethodIndexViewModel, PaymentMethod>()
                .ForMember(x => x.Payments, y => y.Ignore());
            Mapper.CreateMap<PaymentMethodCreateViewModel, PaymentMethod>()
                .ForMember(x => x.Payments, y => y.Ignore())
                .ForMember(x => x.PaymentMethodId, y => y.Ignore());
        }
    }
}
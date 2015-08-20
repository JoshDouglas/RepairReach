//using RepairReach.Data.Repositories;
//using RepairReach.Data.Repositories.Interfaces;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Core.Service;
using RepairReach.Data.Repositories;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.Controllers;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace RepairReach.WebApplication
{
    public class DependencyConfig
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<ICustomerRepository, CustomerRepository>();

            container.RegisterType<ICompanyRepository, CompanyRepository>();

            container.RegisterType<IJobStatusRepository, JobStatusRepository>();

            container.RegisterType<IJobCategoryRepository, JobCategoryRepository>();

            container.RegisterType<IStaffRepository, StaffRepository>();

            container.RegisterType<ITaxRateRepository, TaxRateRepository>();

            container.RegisterType<IServiceRepository, ServiceRepository>();

            container.RegisterType<IJobRepository, JobRepository>();

            container.RegisterType<IApplianceRepository, ApplianceRepository>();

            container.RegisterType<ILineItemRepository, LineItemRepository>();

            container.RegisterType<IPartRepository, PartRepository>();

            container.RegisterType<IJobNoteRepository, JobNoteRepository>();

            container.RegisterType<IPaymentRepository, PaymentRepository>();

            container.RegisterType<IVendorRepository, VendorRepository>();

            container.RegisterType<ITimeClockEntryRepository, TimeClockEntryRepository>();

            container.RegisterType<IAppointmentRepository, AppointmentRepository>();

            container.RegisterType<IActivityEventRepository, ActivityEventRepository>();

            container.RegisterType<IQuickLineItemRepository, QuickLineItemRepository>();

            container.RegisterType<IGeocodingService, GeocodingService>();

            container.RegisterType<IHowDidYouFindUsRepository, HowDidYouFindUsRepository>();

            container.RegisterType<IPaymentMethodRepository, PaymentMethodRepository>();

//            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
//
//            container.RegisterType<ITeamRepository, TeamRepository>();
//
//            container.RegisterType<ICompanyRepository, CompanyRepository>();

            return container;
        }
    }
}
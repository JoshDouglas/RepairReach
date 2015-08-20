//using RepairReach.Data.Repositories;
//using RepairReach.Data.Repositories.Interfaces;

using RepairReach.Data.Repositories;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebAPI.Controllers;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace RepairReach.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

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

//            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
//
//            container.RegisterType<ITeamRepository, TeamRepository>();
//
//            container.RegisterType<ICompanyRepository, CompanyRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
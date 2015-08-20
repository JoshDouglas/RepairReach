using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RepairReach.Data;
using RepairReach.Data.Infrastructure;
//using RepairReach.Data.Infrastructure.Initializers;

namespace RepairReach.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.Initialise();
            AutoMapperConfig.Initialise();

            //DependencyConfig.ResolveDependencies(GlobalConfiguration.Configuration); API
            //GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //Database.SetInitializer<RepairReachContext>(null);
            //Database.SetInitializer(new RepairReachContextInitializer());
        }
    }
}

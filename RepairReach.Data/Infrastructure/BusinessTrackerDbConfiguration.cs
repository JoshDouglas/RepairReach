
using System.Data.Entity;
using System.Data.Entity.SqlServer;
//using RepairReach.Data.Infrastructure.Initializers;

namespace RepairReach.Data.Infrastructure
{
    public class RepairReachDbConfiguration
        : DbConfiguration
    {
        public RepairReachDbConfiguration()
        {
            //SetDatabaseInitializer<RepairReachContext>(new RepairReachContextInitializer());
            //SetDatabaseInitializer<RepairReachUserContext>(new RepairReachUserContextInitializer());
            //Un-comment next line to test execution strategy!
            //AddInterceptor(new ConnectionBreakInterceptor());
        }
    }
}

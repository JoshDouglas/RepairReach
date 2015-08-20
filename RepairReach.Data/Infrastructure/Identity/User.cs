using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RepairReach.Data.Infrastructure.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
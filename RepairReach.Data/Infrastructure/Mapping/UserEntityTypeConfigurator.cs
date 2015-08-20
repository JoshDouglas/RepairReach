
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;
using RepairReach.Data.Infrastructure.Identity;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="User"/>
    /// </summary>
    public class UserEntityTypeConfigurator
        : EntityTypeConfiguration<ApplicationUser>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserEntityTypeConfigurator()
        {
            this.HasOptional(u => u.Staff);
        }
    }
}


using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Service"/>
    /// </summary>
    public class ServiceEntityTypeConfigurator
        : EntityTypeConfiguration<Service>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceEntityTypeConfigurator()
        {
            this.HasKey(s => s.ServiceId);

            this.Property(s => s.ServiceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(s => s.Amount)
                .IsRequired();

            this.Property(s => s.CostAmount)
                .IsOptional();

            this.Property(s => s.Name)
                .IsRequired();

        }
    }
}


using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Appliance"/>
    /// </summary>
    public class ApplianceEntityTypeConfigurator
        : EntityTypeConfiguration<Appliance>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplianceEntityTypeConfigurator()
        {
            this.HasKey(a => a.ApplianceId);

            this.Property(a => a.ApplianceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.ModelNumber)
                .IsOptional();

            this.Property(a => a.SerialNumber)
                .IsOptional();

            this.Property(a => a.ProblemDescription)
                .IsOptional();

            this.Property(a => a.Type)
                .IsRequired();

            this.Property(a => a.Make)
                .IsOptional();

            this.HasRequired(a => a.Job)
                .WithMany(a => a.Appliances)
                .HasForeignKey(a => a.JobId);
            
        }
    }
}

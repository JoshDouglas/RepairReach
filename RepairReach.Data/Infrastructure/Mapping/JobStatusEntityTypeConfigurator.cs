
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="JobStatus"/>
    /// </summary>
    public class JobStatusEntityTypeConfigurator
        : EntityTypeConfiguration<JobStatus>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobStatusEntityTypeConfigurator()
        {
            this.HasKey(a => a.JobStatusId);

            this.Property(a => a.JobStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Description)
                .IsRequired();

            this.Property(a => a.SequenceNumber)
                .IsRequired();

        }
    }
}

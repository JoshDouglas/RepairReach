
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="JobCategory"/>
    /// </summary>
    public class JobCategoryEntityTypeConfigurator
        : EntityTypeConfiguration<JobCategory>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobCategoryEntityTypeConfigurator()
        {
            this.HasKey(a => a.JobCategoryId);

            this.Property(a => a.JobCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Description)
                .IsRequired();

            this.Property(a => a.SequenceNumber)
                .IsRequired();

        }
    }
}

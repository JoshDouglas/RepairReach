
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="JobNote"/>
    /// </summary>
    public class JobNoteEntityTypeConfigurator
        : EntityTypeConfiguration<JobNote>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobNoteEntityTypeConfigurator()
        {
            this.HasKey(a => a.JobNoteId);

            this.Property(a => a.JobNoteId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.CreatedBy)
                .IsRequired();

            this.Property(a => a.CreatedDate)
                .IsRequired();

            this.Property(a => a.Note)
                .IsRequired();
        }
    }
}

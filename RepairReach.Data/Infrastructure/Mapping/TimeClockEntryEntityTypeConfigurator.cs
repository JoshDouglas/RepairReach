
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="JobNote"/>
    /// </summary>
    public class TimeClockEntryEntityTypeConfiguration
        : EntityTypeConfiguration<TimeClockEntry>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TimeClockEntryEntityTypeConfiguration()
        {
            this.HasKey(a => a.TimeClockEntryId);

            this.Property(a => a.TimeClockEntryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.HourlyRate)
                .IsOptional();

            this.Property(a => a.TimeIn)
                .IsOptional();

            this.Property(a => a.TimeOut)
                .IsOptional();

            this.Property(a => a.DatePaid)
                .IsOptional();

            this.HasRequired(a => a.Staff)
                .WithMany(a => a.TimeClockEntries)
                .HasForeignKey(a => a.StaffId);

        }
    }
}

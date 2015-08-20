using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    public class ActivityEventEntityTypeConfigurator : EntityTypeConfiguration<ActivityEvent>
    {
        public ActivityEventEntityTypeConfigurator()
        {
            this.HasKey(a => a.ActivityEventId);
            this.Property(a => a.ActivityEventId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(a => a.JobId)
                .IsRequired();
            this.Property(a => a.EventTime)
                .IsRequired();
            this.Property(a => a.Description)
                .IsRequired();
            this.Property(a => a.CausedBy)
                .IsRequired();
            this.HasRequired(a => a.Job)
                .WithMany(a => a.ActivityEvents)
                .HasForeignKey(a => a.JobId);
        }
    }
}

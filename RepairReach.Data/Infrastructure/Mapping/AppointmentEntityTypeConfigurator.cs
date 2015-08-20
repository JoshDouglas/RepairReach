using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    class AppointmentEntityTypeConfigurator : EntityTypeConfiguration<Appointment>
    {
        public AppointmentEntityTypeConfigurator()
        {
            this.HasKey(a => a.AppointmentId);

            this.Property(a => a.AppointmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.StartTime)
                .IsRequired();

            this.Property(a => a.EndTime)
                .IsRequired();

            this.Property(a => a.Note)
                .IsOptional();

            this.Property(a => a.IsCompleted)
                .IsOptional();

            this.Property(a => a.CompletedTime)
                .IsOptional();

            this.Property(a => a.CreatedBy)
                .IsRequired();

            this.Property(a => a.Created)
                .IsRequired();

            this.Property(a => a.CallOnWay)
                .IsRequired();

            this.Property(a => a.TextOnWay)
                .IsRequired();

            this.HasRequired(a => a.Job)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.JobId);

            this.HasRequired(a => a.Technician)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.TechnicianStaffId);
        }
    }
}

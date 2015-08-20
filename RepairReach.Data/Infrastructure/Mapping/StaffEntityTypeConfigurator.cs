
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Staff"/>
    /// </summary>
    public class StaffEntityTypeConfigurator
        : EntityTypeConfiguration<Staff>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StaffEntityTypeConfigurator()
        {
            this.HasKey(s => s.StaffId);

            this.Property(s => s.StaffId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(s => s.DisplayName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(s => s.Email)
                .IsOptional();

            this.Property(s => s.HourlyRate)
                .IsOptional();

            this.Property(s => s.FirstName)
                .IsRequired();

            this.Property(s => s.LastName)
                .IsRequired();

            this.Property(s => s.UserTitle)
                .IsOptional();

            this.Property(s => s.Phone)
                .IsOptional();

            this.Property(s => s.IsActive)
                .IsOptional();

            this.Property(s => s.ImportedStaffId)
                .IsOptional();

            this.Property(s => s.Username)
                .IsRequired();
        }
    }
}

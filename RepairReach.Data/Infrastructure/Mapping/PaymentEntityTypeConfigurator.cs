
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Payment"/>
    /// </summary>
    public class PaymentEntityTypeConfigurator
        : EntityTypeConfiguration<Payment>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentEntityTypeConfigurator()
        {
            this.HasKey(s => s.PaymentId);

            this.Property(s => s.PaymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(s => s.DatePaid)
                .IsRequired();

            this.Property(s => s.EnteredBy)
                .IsRequired();

            this.Property(s => s.PaymentAmount)
                .IsRequired();

            //this.Property(s => s.PaymentMethod)
            //    .IsRequired();

            this.Property(s => s.Note)
                .IsOptional();

            this.HasRequired(s => s.Job)
                .WithMany(s => s.Payments)
                .HasForeignKey(s => s.JobId)
                .WillCascadeOnDelete(false);

            this.HasRequired(s => s.PaymentMethod)
                .WithMany(s => s.Payments)
                .HasForeignKey(s => s.PaymentMethodId)
                .WillCascadeOnDelete(false);
        }
    }
}

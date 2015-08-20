
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="PaymentMethod"/>
    /// </summary>
    public class PaymentMethodEntityTypeConfigurator
        : EntityTypeConfiguration<PaymentMethod>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentMethodEntityTypeConfigurator()
        {
            this.HasKey(a => a.PaymentMethodId);

            this.Property(a => a.PaymentMethodId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Description)
                .IsRequired();

            this.Property(a => a.SequenceNumber)
                .IsRequired();

        }
    }
}

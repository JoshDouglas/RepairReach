
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="TaxRate"/>
    /// </summary>
    public class TaxRateEntityTypeConfigurator
        : EntityTypeConfiguration<TaxRate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaxRateEntityTypeConfigurator()
        {
            this.HasKey(a => a.TaxRateId);

            this.Property(a => a.TaxRateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Amount)
                .IsRequired();

            this.Property(a => a.IsDefaultRate)
                .IsRequired();

            this.Property(a => a.Name)
                .IsRequired();
            
        }
    }
}

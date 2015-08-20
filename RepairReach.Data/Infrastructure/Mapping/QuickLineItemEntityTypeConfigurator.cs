
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="LineItem"/>
    /// </summary>
    public class QuickLineItemEntityTypeConfigurator
        : EntityTypeConfiguration<QuickLineItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QuickLineItemEntityTypeConfigurator()
        {
            this.HasKey(a => a.QuickLineItemId);

            this.Property(a => a.QuickLineItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Description)
                .IsOptional();

            this.Property(a => a.PartName)
                .IsOptional();

            this.Property(a => a.PartQty)
                .IsOptional();

            this.Property(a => a.PartEach)
                .IsOptional();

            this.Property(a => a.PartCost)
                .IsOptional();

            this.Property(a => a.PartNumber)
                .IsOptional();

            this.Property(a => a.ServiceName)
                .IsOptional();

            this.Property(a => a.ServiceQty)
                .IsOptional();

            this.Property(a => a.ServiceEach)
                .IsOptional();

            this.Property(a => a.ServiceCost)
                .IsOptional();
        }
    }
}

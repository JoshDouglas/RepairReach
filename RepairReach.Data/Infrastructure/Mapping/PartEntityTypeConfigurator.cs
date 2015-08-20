
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Part"/>
    /// </summary>
    public class PartEntityTypeConfigurator
        : EntityTypeConfiguration<Part>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PartEntityTypeConfigurator()
        {
            this.HasKey(s => s.PartId);

            this.Property(s => s.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(s => s.Amount)
                .IsRequired();

            this.Property(s => s.CostAmount)
                .IsOptional();

            this.Property(s => s.Name)
                .IsRequired();

            this.Property(s => s.PartNumber)
                .IsOptional();
        }
    }
}

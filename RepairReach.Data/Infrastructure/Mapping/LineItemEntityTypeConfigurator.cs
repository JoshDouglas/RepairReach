
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="LineItem"/>
    /// </summary>
    public class LineItemEntityTypeConfigurator
        : EntityTypeConfiguration<LineItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LineItemEntityTypeConfigurator()
        {
            this.HasKey(a => a.LineItemId);

            this.Property(a => a.LineItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.LineItemNumber)
                .IsRequired();

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

            //this.Property(a => a.LaborQty)
            //    .IsOptional();

            //this.Property(a => a.LaborEach)
            //    .IsOptional();

            //this.Property(a => a.LaborCost)
            //    .IsOptional();

            this.HasRequired(a => a.Job)
                .WithMany(a => a.LineItems)
                .HasForeignKey(a => a.JobId)
                .WillCascadeOnDelete(false);                

            this.HasRequired(a => a.Technician)
                .WithMany(a => a.LineItems)
                .HasForeignKey(a => a.StaffId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.TaxRate)
                .WithMany(a => a.LineItems)
                .HasForeignKey(a => a.TaxRateId);
            //.WillCascadeOnDelete(false);



        }
    }
}

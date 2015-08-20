
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="JobNote"/>
    /// </summary>
    public class VendorEntityTypeConfiguration
        : EntityTypeConfiguration<Vendor>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VendorEntityTypeConfiguration()
        {
            this.HasKey(a => a.VendorId);

            this.Property(a => a.VendorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.CompanyName)
                .IsRequired();

            this.Property(a => a.CompanyPhone)
                .IsOptional();

            this.Property(a => a.CompanyEmail)
                .IsOptional();

            this.Property(a => a.Address1)
                .IsOptional();

            this.Property(a => a.Address2)
                .IsOptional();

            this.Property(a => a.City)
                .IsOptional();

            this.Property(a => a.State)
                .IsOptional();

            this.Property(a => a.ZipCode)
                .IsOptional();

            this.Property(a => a.Contact1Name)
                .IsOptional();

            this.Property(a => a.Contact1Title)
                .IsOptional();

            this.Property(a => a.Contact1Phone)
                .IsOptional();

            this.Property(a => a.Contact1Email)
                .IsOptional();

            this.Property(a => a.Contact2Name)
                .IsOptional();

            this.Property(a => a.Contact2Title)
                .IsOptional();

            this.Property(a => a.Contact2Phone)
                .IsOptional();

            this.Property(a => a.Contact2Email)
                .IsOptional();
        }
    }
}

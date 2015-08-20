
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Staff"/>
    /// </summary>
    public class CompanyEntityTypeConfigurator
        : EntityTypeConfiguration<Company>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyEntityTypeConfigurator()
        {
            this.HasKey(s => s.CompanyId);

            this.Property(s => s.CompanyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(s => s.Address1)
                .IsRequired();

            this.Property(s => s.Address2)
                .IsOptional();

            this.Property(s => s.City)
                .IsRequired();

            this.Property(s => s.State)
                .IsRequired();

            this.Property(s => s.Name)
                .IsRequired();

            this.Property(s => s.Zipcode)
                .IsRequired();

            this.Property(s => s.TimeZoneInfo)
                .IsRequired();

            this.Property(s => s.Phone)
                .IsOptional();

            this.Property(s => s.Email)
                .IsOptional();

            this.Property(s => s.LogoPath)
                .IsOptional();

            //this.Property(s => s.Logo)
            //    .IsOptional();

            this.Property(s => s.Fax)
                .IsOptional();

            this.Property(s => s.Website)
                .IsOptional();


        }
    }
}

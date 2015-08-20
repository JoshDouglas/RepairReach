
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Customer"/>
    /// </summary>
    public class CustomerEntityTypeConfigurator
        : EntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerEntityTypeConfigurator()
        {
            this.HasKey(c => c.CustomerId);

            this.Property(c => c.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(c => c.Address1)
                .IsRequired();

            this.Property(c => c.Address2)
                .IsOptional();

            this.Property(c => c.City)
                .IsRequired();

            this.Property(c => c.CompanyName)
                .IsOptional();

            this.Property(c => c.Designation)
                .IsRequired();

            this.Property(c => c.Email)
                .IsOptional();

            this.Property(c => c.Fax)
                .IsOptional();

            this.Property(c => c.FirstName)
                .IsOptional();

            this.Property(c => c.LastName)
                .IsOptional();

            this.Property(c => c.Phone1)
                .IsRequired();

            this.Property(c => c.Phone2)
                .IsOptional();

            this.Property(c => c.PrefersTextMessaging)
                .IsOptional();

            this.Property(c => c.State)
                .IsRequired();

            this.Property(c => c.Zipcode)
                .IsRequired();

            this.Property(c => c.CollectPaymentOnSite)
                .IsRequired();

            this.Property(c => c.CallOnWay)
                .IsRequired();

            this.Property(c => c.ImportedCustomerId)
                .IsOptional();

            this.HasOptional(a => a.HowDidYouFindUs)
               .WithMany(a => a.Customers)
               .HasForeignKey(a => a.HowDidYouFindUsId);   
        }
    }
}

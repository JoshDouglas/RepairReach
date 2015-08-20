
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    /// <summary>
    /// The entity type configuration <see cref="Job"/>
    /// </summary>
    public class JobEntityTypeConfigurator
        : EntityTypeConfiguration<Job>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobEntityTypeConfigurator()
        {
            this.HasKey(a => a.JobId);

            this.Property(a => a.JobId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Address1)
                .IsRequired();

            this.Property(a => a.Address2)
                .IsOptional();

            this.Property(a => a.City)
                .IsRequired();

            this.Property(a => a.ContactFirstName)
                .IsRequired();

            this.Property(a => a.ContactLastName)
                .IsRequired();

            this.Property(a => a.ContactPhone1)
                .IsRequired();

            this.Property(a => a.ContactPhone2)
                .IsOptional();

            this.Property(a => a.State)
                .IsRequired();

            this.Property(a => a.Zipcode)
                .IsRequired();

            this.Property(a => a.JobNumber)
                .IsRequired();

            this.Property(a => a.JobCreated)
                .IsRequired();

            this.Property(a => a.LastViewedTime)
                .IsRequired();

            this.Property(a => a.LastViewedBy)
                .IsRequired();

            this.Property(a => a.JobAuthorized)
                .IsOptional();

            this.Property(a => a.JobScheduled)
                .IsOptional();

            this.Property(a => a.JobStarted)
                .IsOptional();

            this.Property(a => a.JobFinished)
                .IsOptional();

            this.Property(a => a.JobClosed)
                .IsOptional();

            this.Property(a => a.JobBilled)
                .IsOptional();

            this.Property(a => a.IsAuthorized)
                .IsOptional();

            this.Property(a => a.ImportedJobId)
                .IsOptional();

            this.HasRequired(a => a.Customer)
                .WithMany(a => a.Jobs)
                .HasForeignKey(a => a.CustomerId);
                //.WillCascadeOnDelete(false);

            this.HasOptional(a => a.JobCategory)
                .WithMany(a => a.Jobs)
                .HasForeignKey(a => a.JobCategoryId);
                //.WillCascadeOnDelete(false);

            //this.HasOptional(a => a.JobNotes);

            this.HasRequired(a => a.JobStatus)
                .WithMany(a => a.Jobs)
                .HasForeignKey(a => a.JobStatusId);
                //.WillCascadeOnDelete(false);

            //this.HasRequired(a => a.LineItems)
                

            this.HasRequired(a => a.SalesRepresentative)
                .WithMany(a => a.Jobs)
                .HasForeignKey(a => a.StaffId)
                .WillCascadeOnDelete(false);
            //.WillCascadeOnDelete(false);
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RepairReach.Core.Model;

namespace RepairReach.Data.Infrastructure.Mapping
{
    class HowDidYouFindUsEntityTypeConfigurator : EntityTypeConfiguration<HowDidYouFindUs>
    {
        public HowDidYouFindUsEntityTypeConfigurator()
        {
            this.HasKey(a => a.HowDidYouFindUsId);

            this.Property(a => a.HowDidYouFindUsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.Description)
                .IsRequired();

            this.Property(a => a.SequenceNumber)
                .IsRequired();
        }
    }
}

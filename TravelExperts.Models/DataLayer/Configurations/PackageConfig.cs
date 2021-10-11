using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TravelExperts.Models
{
    internal class PackageConfig : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> entity)
        {
                entity.HasKey(e => e.PackageId)
                    .HasName("aaaaaPackages_PK")
                    .IsClustered(false);

                entity.Property(e => e.PkgAgencyCommission).HasDefaultValueSql("((0))");
        }
    }
}

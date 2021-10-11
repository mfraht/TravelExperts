using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TravelExperts.Models
{
    internal class PackagesProductsSupplierConfig : IEntityTypeConfiguration<PackagesProductsSupplier>
    {
        public void Configure(EntityTypeBuilder<PackagesProductsSupplier> entity)
        {
                entity.HasKey(e => new { e.PackageId, e.ProductSupplierId })
                    .HasName("aaaaaPackages_Products_Suppliers_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK00");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK01");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TravelExperts.Models
{
    internal class ProductsSupplierConfig : IEntityTypeConfiguration<ProductsSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductsSupplier> entity)
        {
            entity.HasKey(e => e.ProductSupplierId)
                   .HasName("aaaaaProducts_Suppliers_PK")
                   .IsClustered(false);

            entity.Property(e => e.ProductId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductsSuppliers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Products_Suppliers_FK00");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.ProductsSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("Products_Suppliers_FK01");
        }
    }
}

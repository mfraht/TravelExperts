using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TravelExperts.Models
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
                entity.HasKey(e => e.ProductId)
                    .HasName("aaaaaProducts_PK")
                    .IsClustered(false);

                entity.Property(e => e.ProdName).HasDefaultValueSql("((0))");
        }
    }
}

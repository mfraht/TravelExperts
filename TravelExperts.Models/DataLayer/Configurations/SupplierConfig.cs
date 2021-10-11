using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TravelExperts.Models
{
    internal class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.HasKey(e => e.SupplierId)
                .HasName("aaaaaSuppliers_PK")
                .IsClustered(false);
        }
    }
}

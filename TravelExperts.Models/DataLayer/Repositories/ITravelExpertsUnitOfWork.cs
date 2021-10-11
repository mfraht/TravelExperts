namespace TravelExperts.Models
{
    public interface ITravelExpertsUnitOfWork
    {
        Repository<Package> Packages { get; }
        Repository<Product> Products { get; }
        Repository<Supplier> Suppliers { get; }
        Repository<Agency> Agencies { get; }
        Repository<Agent> Agents { get; }
        Repository<ProductsSupplier> ProductsSuppliers { get; }
        Repository<PackagesProductsSupplier> PackagesProductsSuppliers { get; }

        void Save();
    }
}

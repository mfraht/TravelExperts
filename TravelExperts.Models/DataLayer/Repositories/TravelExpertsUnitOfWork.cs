using System.Linq;


namespace TravelExperts.Models
{
    public class TravelExpertsUnitOfWork : ITravelExpertsUnitOfWork
    {
        private TravelExpertsContext context { get; set; }
        public TravelExpertsUnitOfWork(TravelExpertsContext ctx) => context = ctx;

        private Repository<Package> packageData;
        public Repository<Package> Packages
        {
            get {
                if (packageData == null)
                    packageData = new Repository<Package>(context);
                return packageData;
            }
        }

        private Repository<Product> productData;
        public Repository<Product> Products
        {
            get {
                if (productData == null)
                    productData = new Repository<Product>(context);
                return productData;
            }
        }

        private Repository<Supplier> supplierData;
        public Repository<Supplier> Suppliers
        {
            get {
                if (supplierData == null)
                    supplierData = new Repository<Supplier>(context);
                return supplierData;
            }
        }

        private Repository<ProductsSupplier> productsSupplierData;
        public Repository<ProductsSupplier> ProductsSuppliers
        {
            get {
                if (productsSupplierData == null)
                    productsSupplierData = new Repository<ProductsSupplier>(context);
                return productsSupplierData;
            }
        }

        private Repository<PackagesProductsSupplier> packagesProductsSupplierData;
        public Repository<PackagesProductsSupplier> PackagesProductsSuppliers
        {
            get
            {
                if (packagesProductsSupplierData == null)
                    packagesProductsSupplierData = new Repository<PackagesProductsSupplier>(context);
                return packagesProductsSupplierData;
            }
        }

        private Repository<Agency> agencyData;
        public Repository<Agency> Agencies
        {
            get
            {
                if (agencyData == null)
                    agencyData = new Repository<Agency>(context);
                return agencyData;
            }
        }

        private Repository<Agent> agentData;
        public Repository<Agent> Agents
        {
            get
            {
                if (agentData == null)
                    agentData = new Repository<Agent>(context);
                return agentData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
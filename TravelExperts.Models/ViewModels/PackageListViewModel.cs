using System.Collections.Generic;


namespace TravelExperts.Models
{
    public class PackageListViewModel 
    {
        public IEnumerable<Package> Packages { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under1000", "Under $1000" },
                { "1Kto3K", "$1000 to $3000" },
                { "over3K", "Over $3000" }
            };
    }
}

using System.Collections.Generic;


namespace TravelExperts.Models
{
    public class SupplierListViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}

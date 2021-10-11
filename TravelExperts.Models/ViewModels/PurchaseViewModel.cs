using System.Collections.Generic;
using TravelExperts.Models.DomainModels;


namespace TravelExperts.Models
{
    public class PurchaseViewModel
    {
        public IEnumerable<PurchaseItem> List { get; set; }
        public double Subtotal { get; set; }
        public RouteDictionary PackageGridRoute { get; set; }
    }
}

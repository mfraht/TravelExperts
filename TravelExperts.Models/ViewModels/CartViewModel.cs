using System.Collections.Generic;


namespace TravelExperts.Models
{
    public class CartViewModel 
    {
        public IEnumerable<CartItem> List { get; set; }
        public double Subtotal { get; set; }
        public RouteDictionary PackageGridRoute { get; set; }
    }
}

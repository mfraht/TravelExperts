using Newtonsoft.Json;
using TravelExperts.Models;

namespace TravelExperts.Models
{
    public class CartItem
    {
        public PackageDTO Package { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => (double)Package.PkgBasePrice * Quantity;
    }
}

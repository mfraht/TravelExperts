using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelExperts.Models.DomainModels
{
    public class PurchaseItem
    {
        public PackageDTO Package { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => (double)Package.PkgBasePrice * Quantity;
    }
}

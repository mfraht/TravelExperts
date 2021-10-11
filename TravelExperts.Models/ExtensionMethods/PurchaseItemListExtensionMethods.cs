using System.Linq;
using System.Collections.Generic;
using TravelExperts.Models.DomainModels;


namespace TravelExperts.Models
{
    public static class PurchaseItemListExtension
    {
        public static List<PurchaseItemDTO> ToDTO(this List<PurchaseItem> list) =>
            list.Select(ci => new PurchaseItemDTO
            {
                PackageId = ci.Package.PackageId,
                Quantity = ci.Quantity
            }).ToList();
    }
}

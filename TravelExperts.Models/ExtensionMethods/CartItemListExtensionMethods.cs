using System.Linq;
using System.Collections.Generic;


namespace TravelExperts.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO {
                PackageId = ci.Package.PackageId,
                Quantity = ci.Quantity
            }).ToList();
    }
}

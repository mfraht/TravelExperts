using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;


namespace TravelExperts.Models.DomainModels
{
    public class Purchase
    {
        private const string PurchaseKey = "mypurchase";
        private const string CountKey = "mycount";

        private List<PurchaseItem> items { get; set; }
        private List<PurchaseItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public Purchase(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }

        public void Load(Repository<Package> data)
        {
            items = session.GetObject<List<PurchaseItem>>(PurchaseKey);
            if (items == null)
            {
                items = new List<PurchaseItem>();
                storedItems = requestCookies.GetObject<List<PurchaseItemDTO>>(PurchaseKey);
            }
            if (storedItems?.Count > items?.Count)
            {
                foreach (PurchaseItemDTO storedItem in storedItems)
                {
                    var package = data.Get(new QueryOptions<Package>
                    {
                        //Include = "BookAuthors.Author, Genre",
                        Where = b => b.PackageId == storedItem.PackageId
                    });
                    if (package != null)
                    {
                        var dto = new PackageDTO();
                        dto.Load(package);

                        PurchaseItem item = new PurchaseItem
                        {
                            Package = dto,
                            Quantity = storedItem.Quantity
                        };
                        items.Add(item);
                    }
                }
                Save();
            }
        }

        public double Subtotal => items.Sum(i => i.Subtotal);
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<PurchaseItem> List => items;

        public PurchaseItem GetById(int id) =>
            items.FirstOrDefault(ci => ci.Package.PackageId == id);

        public void Add(PurchaseItem item)
        {
            var itemInPurchase = GetById(item.Package.PackageId);

            if (itemInPurchase == null)
            {
                items.Add(item);
            }
            else
            {
                itemInPurchase.Quantity += 1;
            }
        }

        public void Edit(PurchaseItem item)
        {
            var itemInPurchase = GetById(item.Package.PackageId);
            if (itemInPurchase != null)
            {
                itemInPurchase.Quantity = item.Quantity;
            }
        }

        public void Remove(PurchaseItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(PurchaseKey);
                session.Remove(CountKey);
                responseCookies.Delete(PurchaseKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<PurchaseItem>>(PurchaseKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<PurchaseItemDTO>>(PurchaseKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}

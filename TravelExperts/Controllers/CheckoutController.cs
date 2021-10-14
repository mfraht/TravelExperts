// -------------------------------------------------------------------------------- //
// --------------------------- CHECKOUT CONTROLLER -------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperts.Models;
using TravelExperts.Models.DomainModels;


namespace TravelExperts.Controllers
{
    // ----------------------- Definition of Checkout Controller class ----------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    [Authorize]
    public class CheckoutController : Controller
    {
        // --------------------------- Fetch data from Repository ---------------------------------- //
        private Repository<Package> data { get; set; }
        public CheckoutController(TravelExpertsContext ctx) => data = new Repository<Package>(ctx);


        // --------------------------- Function to Get Cart items ---------------------------------------- //
        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
        
            return cart;
        }


        // --------------------------- Function to View Purchased items ---------------------------------------- //
        private Purchase GetPurchase()
        {
            var purchase = new Purchase(HttpContext);
            purchase.Load(data);
         
            return purchase;
        }


        // --------------------------- Function to View Purchased items -------------------------------- //
        public ViewResult Index()
        {
            var purchase = GetPurchase();
            var builder = new PackagesGridBuilder(HttpContext.Session);

            var vm = new PurchaseViewModel   // -------- View Model
            {
                List = purchase.List,
                Subtotal = purchase.Subtotal,
                PackageGridRoute = builder.CurrentRoute
            };

            return View(vm);
        }


        // --------------------------- Function to Add items to Purchase -------------------------------- //
        public RedirectToActionResult Add(int id)
        {
            var package = data.Get(new QueryOptions<Package>
            {
                Where = b => b.PackageId == id
            });
            if (package == null)
            {
                TempData["message"] = "Unable to add package to Purchase.";
            }
            else
            {
                var dto = new PackageDTO();
                dto.Load(package);
                PurchaseItem item = new PurchaseItem
                {
                    Package = dto,
                    Quantity = 1  // -------- default value
                };
        
                Cart cart = GetCart();
                CartItem cartItem = cart.GetById(id);
                cart.Remove(cartItem);
                cart.Save();
                
                Purchase purchase = GetPurchase();
                purchase.Add(item);
                purchase.Save();

                // -------------- Display message when item added to cart
                TempData["message"] = $"{package.PkgName} added to Purchase.";
            }

            var builder = new PackagesGridBuilder(HttpContext.Session);
            return RedirectToAction("Index", "Checkout", builder.CurrentRoute);
        }


        // --------------------------- POST Function to Remove items from Purchase -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Purchase purchase = GetPurchase();
            PurchaseItem item = purchase.GetById(id);

            // ----- Allow removing items from Purchase only if the start date is in future
            if (item.Package.PkgStartDate < DateTime.Now)
            {
                // ------------- Display message when item(s) cannot be removed from purchase
                TempData["message"] = "Unable to Remove package, the start date has passed.";
            }
            else
            {
                purchase.Remove(item);
                purchase.Save();

                // -------------------- Display message when item removed from purchase
                TempData["message"] = $"{item.Package.PkgName} removed from purchase.";
            }

            return RedirectToAction("Index");
        }

        
        // --------------------------- POST Function to Clear all items from Purchase -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Purchase purchase = GetPurchase();
            purchase.Clear();
            purchase.Save();

            // -------------- Display message when item cleared from purchase
            TempData["message"] = "Purchase cleared.";
            return RedirectToAction("Index");
        }


        // --------------------------- GET Function to Edit items in Purchase -------------------------------- //
        public IActionResult Edit(int id)
        {
            Purchase purchase = GetPurchase();
            PurchaseItem item = purchase.GetById(id);
            
            if (item == null)
            {
                TempData["message"] = "Unable to locate purchase item";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }


        // --------------------------- POST Function to Edit items in Purchase -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Edit(PurchaseItem item)
        {
            Purchase purchase = GetPurchase();
            purchase.Edit(item);
            purchase.Save();

            // -------------- Display message after purchase items updated
            TempData["message"] = $"{item.Package.PkgName} updated";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Proceed to Pay after Purchase -------------------------------- //
        public ViewResult Pay() => View();


        // ----------------------- Definition of Checkout Controller class ------------------------------ //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}

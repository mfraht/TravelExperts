// -------------------------------------------------------------------------------- //
// ------------------------------- CART CONTROLLER -------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelExperts.Models;


namespace TravelExperts.Controllers
{
    // --------------------------- Definition of Cart Controller class ----------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    [Authorize]
    public class CartController : Controller
    {
        // --------------------------- Fetch data from Repository ---------------------------------- //
        private Repository<Package> data { get; set; }
        public CartController(TravelExpertsContext ctx) => data = new Repository<Package>(ctx);


        // --------------------------- Function to Get Cart items ---------------------------------------- //
        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);

            return cart;
        }


        // --------------------------- Function to Read Cart data -------------------------------- //
        public ViewResult Index()
        {
            var cart = GetCart();
            var builder = new PackagesGridBuilder(HttpContext.Session);

            var vm = new CartViewModel   // -------- View Model
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                PackageGridRoute = builder.CurrentRoute
            };

            return View(vm);
        }


        // --------------------------- Function to Add items to Cart -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var package = data.Get(new QueryOptions<Package>
            {
                Where = b => b.PackageId == id
            });
            
            if (package == null)
            {
                TempData["message"] = "Unable to add package to cart.";
            }
            else
            {
                var dto = new PackageDTO();
                dto.Load(package);

                CartItem item = new CartItem
                {
                    Package = dto,
                    Quantity = 1  // -------- default value
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                // -------------- Display message when item added to cart
                TempData["message"] = $"{package.PkgName} added to cart";
            }

            var builder = new PackagesGridBuilder(HttpContext.Session);
            return RedirectToAction("Index", "Cart", builder.CurrentRoute);
        }


        // --------------------------- Function to Remove items from Cart -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            // -------------------- Display message when item removed from cart
            TempData["message"] = $"{item.Package.PkgName} removed from cart.";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Clear all items from Cart -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            // -------------- Display message when item cleared from cart
            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }


        // --------------------------- GET Function to Edit items in Cart -------------------------------- //
        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            
            if (item == null)
            {
                // -------------- Display message when no items in cart
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }


        // --------------------------- POST Function to Edit items in Cart -------------------------------- //
        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            // -------------- Display message after cart items updated
            TempData["message"] = $"{item.Package.PkgName} updated";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Checkout after Shopping -------------------------------- //
        public ViewResult Checkout() => View();


        // --------------------------- Definition of Cart Controller class ------------------------------ //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}
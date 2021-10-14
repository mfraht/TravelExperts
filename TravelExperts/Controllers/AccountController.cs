// -------------------------------------------------------------------------------- //
// ----------------------------- ACCOUNT CONTROLLER ------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //


using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelExperts.Models;


namespace TravelExperts.Controllers
{
    // ------------------------- Definition of Account Controller class ---------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
        }


        // --------------------------- GET Function to Register user account -------------------------------- //
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        // --------------------------- POST Function to Register user account -------------------------------- //
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TravelExperts.Models.User { 
                    UserName = model.Username,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);

                // ------------------- If User successfully registers, log them in ----------------------- //
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent : false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        // --------------------------- Function for Log Out -------------------------------- //
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        
        // --------------------------- GET Function for Log In -------------------------------- //
        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }


        // --------------------------- POST Function for Log In -------------------------------- //
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                // ------------------------ If Username & Password Match -------------------------------- //
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // ------------------------ If Username & Password Do Not Match -------------------------------- //
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }


        // --------------------------- Function for Access Denied -------------------------------- //
        public ViewResult AccessDenied()
        {
            return View();
        }

        // ------------------------- Definition of Account Controller class ----------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}

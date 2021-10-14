// -------------------------------------------------------------------------------- //
// ----------------------------- PACKAGE CONTROLLER ------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelExperts.Models;


namespace TravelExperts.Areas.Admin.Controllers
{
    // --------------------------- State Roles that will access this class --------------------------- //
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PackageController : Controller
    {
    // --------------------------- Definition of Package Controller class --------------------------- //
    // ---------------------------------------- start ----------------------------------------------- //


        // --------------------------- Fetch data from Unit of Work ------------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public PackageController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);

        
        // --------------------------- GET Function to Read Package data -------------------------------- //
        public ActionResult Index()
        {
            var q = new QueryOptions<Package>();
            q.OrderBy = s => s.PackageId;

            var packages = data.Packages.List(q);
            return View("List", packages);
        }

        // --------------------------- Function to Add Package data -------------------------------- //
        [HttpGet]
        public ViewResult Add(int id) => GetPackage(id, "Add");

        [HttpPost]
        public IActionResult Add(PackageViewModel vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Packages.Insert(vm.Package);
                data.Save();

                // -------- Display message upon Insert
                TempData["message"] = $"{vm.Package.PkgName} added to Packages.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Package", vm);
            }
        }


        // --------------------------- Function to Update Package data -------------------------------- //
        [HttpGet]
        public ViewResult Edit(int id) => GetPackage(id, "Edit");

        [HttpPost]
        public IActionResult Edit(PackageViewModel vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Packages.Update(vm.Package);
                data.Save();

                // -------- Display message upon Update
                TempData["message"] = $"{vm.Package.PkgName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Package", vm);
            }
        }


        // --------------------------- Function to Delete Package data -------------------------------- //
        [HttpGet]
        public ViewResult Delete(int id) => GetPackage(id, "Delete");

        [HttpPost]
        public IActionResult Delete(PackageViewModel vm)  // -------- View Model
        {
            data.Packages.Delete(vm.Package);
            data.Save();

            // -------- Display message upon Update
            TempData["message"] = $"{vm.Package.PkgName} removed from Packages.";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Load Package data -------------------------------- //
        private ViewResult GetPackage(int id, string operation)
        {
            var Package = new PackageViewModel();
            Load(Package, operation, id);

            return View("Package", Package);
        }


        // --------------------------- Function to Load Package data -------------------------------- //
        private void Load(PackageViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Package = new Package();
            }
            else
            {
                vm.Package = data.Packages.Get(new QueryOptions<Package>
                {
                    Where = b => b.PackageId == (id ?? vm.Package.PackageId)
                });
            }

            vm.Products = data.Products.List(new QueryOptions<Product>
            {
                OrderBy = a => a.ProdName
            });
        }

        // ------------------------- Definition of Package Controller class ----------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //   
    }
}

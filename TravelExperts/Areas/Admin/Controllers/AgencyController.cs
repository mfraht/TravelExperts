// -------------------------------------------------------------------------------- //
// ------------------------------ AGENCY CONTROLLER ------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Models;


namespace TravelExperts.Areas.Admin.Controllers
{
    // --------------------------- State Roles that will access this class --------------------------- //
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AgencyController : Controller
    {
    // --------------------------- Definition of Agency Controller class ---------------------------- //
    // ---------------------------------------- start ----------------------------------------------- //

        public Agency selAgency = new Agency();

        // --------------------------- Fetch data from Unit of Work -------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public AgencyController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);


        // --------------------------- Function to Read Agency data -------------------------------- //
        public ActionResult Index()
        {
            var q = new QueryOptions<Agency>();
            q.OrderBy = s => s.AgencyId;

            var agencies = data.Agencies.List(q);
            return View("List", agencies);
        }


        // -------------------- Function to GET: AgencyController/Details ------------------------- //
        public ActionResult Details(int id)
        {
            var agency = data.Agencies.Get(new QueryOptions<Agency>
            {
                Where = b => b.AgencyId == id
            });
            return View(agency);
        }


        // --------------------------- Function to Add Agency data -------------------------------- //
        [HttpGet]
        public ViewResult Add(int id) => GetAgency(id, "Add");

        [HttpPost]
        public IActionResult Add(Agency vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Agencies.Insert(vm);
                data.Save();

                // -------- Display message upon Insert
                TempData["message"] = $"{vm.AgncyCity} added to Agencies.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Agency", vm);
            }
        }


        // --------------------------- Function to Update Agency data -------------------------------- //
        [HttpGet]
        public ViewResult Edit(int id) => GetAgency(id, "Edit");

        [HttpPost]
        public IActionResult Edit(Agency vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Agencies.Update(vm);
                data.Save();

                // -------- Display message upon Update
                TempData["message"] = $"{vm.AgncyCity} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Agency", vm);
            }
        }


        // --------------------------- Function to Delete Agency data -------------------------------- //
        [HttpGet]
        public ViewResult Delete(int id) => GetAgency(id, "Delete");

        [HttpPost]
        public IActionResult Delete(Agency vm)
        {
            data.Agencies.Delete(vm);
            data.Save();

            // -------- Display message upon Deletion
            TempData["message"] = $"{vm.AgncyCity} removed from Agencies.";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Load Agency data -------------------------------- //
        private ViewResult GetAgency(int id, string operation)
        {
            Load(selAgency, operation, id);
            return View("Agency", selAgency);
        }


        // --------------------------- Function to Load Agency data -------------------------------- //
        private void Load(Agency vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm = new Agency();
            }
            else
            {
                vm = data.Agencies.Get(new QueryOptions<Agency>
                {
                    Where = b => b.AgencyId == (id ?? vm.AgencyId)
                });
            }
            selAgency = vm;
        }

        // --------------------------- Definition of Agency Controller class ---------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //
    }
}

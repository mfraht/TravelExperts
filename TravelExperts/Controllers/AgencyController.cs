// -------------------------------------------------------------------------------- //
// ----------------------------- ACCOUNT CONTROLLER ------------------------------- //
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


namespace TravelExperts.Controllers
{
    // -------------------------- Definition of Agency Controller class ---------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    public class AgencyController : Controller
    {
        // --------------------------- Fetch data from Unit of Work -------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public AgencyController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);


        // --------------------------- Function to Read Agency data -------------------------------- //
        public ActionResult Index()
        {
            var q = new QueryOptions<Agency>();
            q.OrderBy = s => s.AgencyId;

            var agencies = data.Agencies.List(q);
            return View(agencies);
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


        // -------------------- Function to GET: AgencyController/Create ------------------------- //
        public ActionResult Create()
        {
            return View();
        }


        // -------------------- Function to POST: AgencyController/Create ------------------------- //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // --------------------------- Function to Update Agency data -------------------------------- //
        public ActionResult Edit(int id)
        {
            return View();
        }


        // -------------------- Function to POST: AgencyController/Edit ------------------------- //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        // --------------------------- Function to Delete Agency data -------------------------------- //
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        // -------------------- Function to POST: AgencyController/Delete ------------------------- //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // --------------------------- Definition of Agency Controller class ---------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //
    }
}

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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Models;


namespace TravelExperts.Controllers
{
    // --------------------------- Definition of Package Controller class --------------------------- //
    // ---------------------------------------- start ----------------------------------------------- //
    public class PackageController : Controller
    {
        // --------------------------- Fetch data from Unit of Work ------------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public PackageController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);


        // --------------------------- GET Function to Read Package data -------------------------------- //
        public ViewResult Index(PackagesGridDTO values)
        {
            var q = new QueryOptions<Package>();
            q.OrderBy = s => s.PackageId;

            var packages = data.Packages.List(q);
            return View(packages);
        }


        // --------------------------- GET Function to Read Package details -------------------------------- //
        public ActionResult Details(int id)
        {
            var package = data.Packages.Get(new QueryOptions<Package>
            {
                Where = b => b.PackageId == id
            });
            
            return View(package);
        }


        // --------------------------- GET Function to Create Package -------------------------------- //
        public ActionResult Create()
        {
            return View();
        }


        // --------------------------- POST Function to Create Package -------------------------------- //
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


        // --------------------------- GET Function to Edit Package -------------------------------- //
        public ActionResult Edit(int id)
        {
            return View();
        }


        // --------------------------- POST Function to Edit Package -------------------------------- //
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


        // --------------------------- GET Function to Delete Package -------------------------------- //
        public ActionResult Delete(int id)
        {
            return View();
        }


        // --------------------------- POST Function to Delete Package -------------------------------- //
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

        // ------------------------- Definition of Package Controller class ----------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //   
    }
}

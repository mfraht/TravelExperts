// -------------------------------------------------------------------------------- //
// ------------------------------ AGENT CONTROLLER ------------------------------- //
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
    // --------------------------- Definition of Agent Controller class ---------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    public class AgentController : Controller
    {
        // --------------------------- Fetch data from Unit of Work -------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public AgentController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);


        // --------------------------- Function to Read Agent data -------------------------------- //
        public ActionResult Index()
        {
            var q = new QueryOptions<Agent>();
            q.OrderBy = s => s.AgentId;

            var agents = data.Agents.List(q);
            return View(agents);
        }

        
        // -------------------- Function to GET: AgentController/Details ------------------------- //
        public ActionResult Details(int id)
        {
            var agent = data.Agents.Get(new QueryOptions<Agent>
            {
                Where = b => b.AgentId == id
            });
        
            return View(agent);
        }


        // -------------------- Function to GET: AgentController/Create ------------------------- //
        public ActionResult Create()
        {
            return View();
        }


        // -------------------- Function to POST: AgentController/Create ------------------------- //
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


        // -------------------- Function to GET: AgentController/Edit ------------------------- //
        public ActionResult Edit(int id)
        {
            return View();
        }


        // -------------------- Function to POST: AgentController/Edit ------------------------- //
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


        // -------------------- Function to GET: AgentController/Delete ------------------------- //
        public ActionResult Delete(int id)
        {
            return View();
        }


        // -------------------- Function to POST: AgentController/Delete ------------------------- //
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

        // --------------------------- Definition of Agent Controller class ----------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}

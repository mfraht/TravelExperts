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
using TravelExperts.Models;


namespace TravelExperts.Areas.Admin.Controllers
{
    // --------------------------- State Roles that will access this class --------------------------- //
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AgentController : Controller
    {
    // --------------------------- Definition of Agent Controller class ---------------------------- //
    // --------------------------------------- start ----------------------------------------------- //

        public Agent selAgent = new Agent();

        // --------------------------- Fetch data from Unit of Work -------------------------------- //
        private TravelExpertsUnitOfWork data { get; set; }
        public AgentController(TravelExpertsContext ctx) => data = new TravelExpertsUnitOfWork(ctx);


        // --------------------------- Function to Read Agent data -------------------------------- //
        public ActionResult Index()
        {
            var q = new QueryOptions<Agent>();
            q.OrderBy = s => s.AgentId;

            var agents = data.Agents.List(q);
            return View("List", agents);
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


        // --------------------------- Function to Add Agent data -------------------------------- //
        [HttpGet]
        public ViewResult Add(int id) => GetAgent(id, "Add");

        [HttpPost]
        public IActionResult Add(Agent vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Agents.Insert(vm);
                data.Save();

                // -------- Display message upon Insert
                TempData["message"] = $"{vm.AgtFullName} added to Agents.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Agent", vm);
            }
        }


        // --------------------------- Function to Update Agent data -------------------------------- //
        [HttpGet]
        public ViewResult Edit(int id) => GetAgent(id, "Edit");

        [HttpPost]
        public IActionResult Edit(Agent vm)  // -------- View Model
        {
            if (ModelState.IsValid)
            {
                data.Agents.Update(vm);
                data.Save();

                // -------- Display message upon Update
                TempData["message"] = $"{vm.AgtFullName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Agent", vm);
            }
        }


        // --------------------------- Function to Delete Agent data -------------------------------- //
        [HttpGet]
        public ViewResult Delete(int id) => GetAgent(id, "Delete");

        [HttpPost]
        public IActionResult Delete(Agent vm)
        {
            data.Agents.Delete(vm);
            data.Save();

            // -------- Display message upon Deletion
            TempData["message"] = $"{vm.AgtFullName} removed from Agents.";
            return RedirectToAction("Index");
        }


        // --------------------------- Function to Load Agent data -------------------------------- //
        private ViewResult GetAgent(int id, string operation)
        {
            Load(selAgent, operation, id);
            return View("Agent", selAgent);
        }


        // --------------------------- Function to Load Agent data -------------------------------- //
        private void Load(Agent vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm = new Agent();
            }
            else
            {
                vm = data.Agents.Get(new QueryOptions<Agent>
                {
                    Where = b => b.AgentId == (id ?? vm.AgentId)
                });
            }
            selAgent = vm;
        }

        // --------------------------- Definition of Agent Controller class ----------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}

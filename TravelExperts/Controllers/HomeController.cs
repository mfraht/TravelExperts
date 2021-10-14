// -------------------------------------------------------------------------------- //
// ----------------------------- HOME CONTROLLER ---------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Models;


namespace TravelExperts.Controllers
{
    // ----------------------- Definition of Home Controller class --------------------------------- //
    // --------------------------------------- start ----------------------------------------------- //
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        // --------------------------- Function to Load Home page ---------------------------------------- //
        public IActionResult Index()
        {
            return View();
        }


        // --------------------------- Function to Load Privacy Page ---------------------------------------- //
        public IActionResult Privacy()
        {
            return View();
        }


        // --------------------------- Function to Load Error Page ---------------------------------------- //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
        // ----------------------- Definition of Home Controller class ---------------------------------- //
        // ---------------------------------------- end ------------------------------------------------- //    
    }
}

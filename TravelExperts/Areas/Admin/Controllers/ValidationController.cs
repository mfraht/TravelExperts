using Microsoft.AspNetCore.Mvc;
using TravelExperts.Models;

namespace TravelExperts.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<User> userData { get; set; }
        private Repository<Agent> agentData { get; set; }

        public ValidationController(TravelExpertsContext ctx)
        {
            userData = new Repository<User>(ctx);
            agentData = new Repository<Agent>(ctx);
        }

        public JsonResult CheckAgent(string agentId)
        {
            var validate = new Validate(TempData);
            validate.CheckAgent(agentId, agentData);
            if (validate.IsValid)
            {
                validate.MarkAgentChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckUser(string firstName, string lastName, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckUser(firstName, lastName, operation, userData);
            if (validate.IsValid)
            {
                validate.MarkUserChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }
    }
}
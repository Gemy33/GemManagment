using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Plan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GemManagment.PL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public IActionResult Index()
        {
            var plans = planService.GetAllPlans();
            return View(plans);
        }
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction("Index");
            }
            var plan = planService.GetPlanById(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction("Index");

            }
            return View(plan);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction("Index");
            }
            var plan = planService.DisplayToEdit(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found or cannot be edited.";
                return RedirectToAction("Index");
            }
            return View(plan);

        }
        [HttpPost]
        public IActionResult Edit(int id, PlanToEditViewModel model)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors and try again.");
                return View(model);
            }
            var success = planService.EditPlan(id, model);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to update the plan. It may have active members or an error occurred.";
                return View(model);
            }
            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult ToggleStatus(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction("Index");
            }

            var done = planService.TogglePlanStatus(id);
            if (!done)
            {
                TempData["ErrorMessage"] = "Failed to toggle plan status. It may have active members or an Not found occurred.";
            }
            else
            {
                TempData["SuccessMessage"] = "Plan status toggled successfully.";
            }
            return RedirectToAction("Index");


        }
    }


}

using GemManagment.BLL.Services.Implementaion;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Trainer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GemManagment.PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TraineerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TraineerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var allTriners = trainerService.GetAllTrainers();
            return View(allTriners);
        }
        public ActionResult GetDetails(int id)
        {
            var trainerDetails = trainerService.GetTrainerById(id);
            return View(trainerDetails);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TrainerToCreateViewModel trainerToCreate) // creat triner view model
        {
            if (trainerService.IsEmailExist(trainerToCreate.Email))
            {
                ModelState.AddModelError("email", $"this {trainerToCreate.Email} Aready Exist");
                return View(trainerToCreate);

            }
            if (trainerService.IsPhoneExist(trainerToCreate.PhoneNumber))
            {
                ModelState.AddModelError("phone", $"this {trainerToCreate.PhoneNumber} Aready Exist");
                return View(trainerToCreate);

            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ceateerror", "Please correct the errors and try again.");
                return View(trainerToCreate);
            }

            var created = trainerService.CreateTrainer(trainerToCreate);
            if (!created)
            {
                TempData["error"] = "faile to create";
                return RedirectToAction(nameof(Index));
            }

            TempData["Succees"] = "Triner Created Successfuly";
            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["error"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainerToEdit = trainerService.GetTrainerToUpdate(id);
            if (trainerToEdit is null)
            {
                TempData["error"] = "not found trainer to update";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerToEdit);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            var trainerToEdit = trainerService.UpdateTrainer(id, trainerToUpdate);
            if (!trainerToEdit)
            {
                TempData["error"] = "filed to update please try again";
                return RedirectToAction(nameof(Index));
            }
            TempData["Succees"] = "Trainer updated Successfuly";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(int id )
        {
            if (id <= 0)
            {
                TempData["error"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = trainerService.GetTrainerById(id);
            if (trainerDetails is null)
            {
                TempData["error"] = "not found trainer to delete";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;

            return View();

        }
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {

            if (id <= 0)
            {
                TempData["error"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var deletedMember = trainerService.DeleteTrainer(id);
            if (!deletedMember)
            {
                TempData["error"] = "Failed To Delete Trainer , Please Try Again";
                return RedirectToAction(nameof(Index));
            }
            TempData["Succees"] = "Trainer Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }

    }
}

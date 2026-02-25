using GemManagment.BLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GemManagment.PL.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        // GET: SessionController
        public ActionResult Index()
        {
            var sessions = sessionService.GetAllSessions();
            return View(sessions);
        }

        // GET: SessionController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }
            var session = sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: SessionController/Create
        public ActionResult Create()
        {
            // get all categories and trainers to fill dropdowns
            FillCategoryDropDwon();
            if (!FillTrainerDropDwon())
            {
                TempData["ErrorMessage"] = "No trainers available. Please add trainers before creating a session.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        void FillCategoryDropDwon()
        {
            var categoris = sessionService.GetCategoryDropDownViewModels();
            var listCate = new SelectList(categoris, "Id", "Name");
            ViewBag.Categories = listCate;
        }
        bool FillTrainerDropDwon()
        {
            var trainers = sessionService.GetTrainerDropDownViewModels();
            if (trainers == null || !trainers.Any())
                return false;
            var listTrainer = new SelectList(trainers, "Id", "Name");
            ViewBag.Trainers = listTrainer;
            return true;
        }

        // POST: SessionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please fill all required fields correctly.");
                FillCategoryDropDwon();
                FillTrainerDropDwon();
                return View(createSession);
            }
            var isCreated = sessionService.CreateSession(createSession);
            if (isCreated)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create session. Please try again.";

                return RedirectToAction(nameof(Index));
            }

        }

        // GET: SessionController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!FillTrainerDropDwon())
            {
                TempData["ErrorMessage"] = "No trainers available. Please add trainers before editing a session.";
                return RedirectToAction(nameof(Index));
            }
            var session = sessionService.GetSessionForUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Error The Sesion May be Started Or Not Found!.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // POST: SessionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateSessionViewModel updateSession)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(updateSession);
            }
            var existingSession = sessionService.GetSessionById(id);
            if (existingSession == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            var isUpdated = sessionService.UpdateSession(id, updateSession);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update session. Please try again.";
                return View(updateSession);
            }
        }

        // GET: SessionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(Index));
            }
            var session = sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;

            return View();
        }

        // POST: SessionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           var deleted = sessionService.DeleteSession(id);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Session deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete session. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

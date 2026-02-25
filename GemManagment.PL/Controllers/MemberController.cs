using GemManagment.BLL.Services.AttachmentService;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GemManagment.PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly ImemberService memberService;

        public MemberController(ImemberService memberService )
        {
            this.memberService = memberService;
        }
        #region Index
        public IActionResult Index(string? search, int page = 1)
        {
            int pageSize = 5;

            var query = memberService.GetAllMembrs().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(m =>
                    m.Name.ToLower().Contains(search) ||
                    m.Email.ToLower().Contains(search) ||
                    m.Phone.ToLower().Contains(search));
            }

            int totalItems = query.Count();

            var members = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.Search = search;

            
            return View(members);

        }
        #endregion

        #region Details
        public ActionResult GetMemberDetails(int id)
        {
            if(id <=0)
            {
                TempData["Error"] = "Invalid Member Id";
                return View(nameof(Index));
            }
            var member = memberService.GetMemberDetails(id);
            if (member is null)
            {
                TempData["Error"] = "Member Not Found";
                return View(nameof(Index));

            }
            return View(member);
        }
        public ActionResult HealtheRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid Member Id";
                return View(nameof(Index));
            }
            var healthRecord = memberService.GetMemberHealthDetails(id);
            if (healthRecord is null)
            {
                TempData["Error"] = "Member Not Found";
                return View(nameof(Index));

            }
            return View(healthRecord);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMemberViewModel createMemberView)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Data , Please Re-Enter Data";
                return RedirectToAction(nameof(Index));
            }

            var created = memberService.CreateMember(createMemberView);
            if (!created)
            {
                TempData["Error"] = "Failed To Create Member , Please Try Again";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Member Created Successfully";

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete([FromRoute]int id)
        {
            if (id <= 0)
                {
                TempData["Error"] = "Invalid Member Id";
                return RedirectToAction(nameof(Index));
            }
            var deletedMember = memberService.RemoveMember(id);
            if (!deletedMember)
            {
                TempData["Error"] = "Failed To Delete Member , Please Try Again";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Member Deleted Successfully";
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid Member Id";
                return RedirectToAction(nameof(Index));
            }
            var memberForUpdate = memberService.GetMemberForUpdate(id);
            if (memberForUpdate is null)
            {
                TempData["Error"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(memberForUpdate);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute]int id ,UpdatedMemberViewModel updatedMember)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedMember);
            }

            var deletedMember = memberService.UpdateMember(id, updatedMember);
            if (!deletedMember)
            {
                TempData["Error"] = "Failed To Update Member , Please Try Again";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Member Updated Successfully";
            return RedirectToAction(nameof(Index));

        } 
        #endregion
    }
}

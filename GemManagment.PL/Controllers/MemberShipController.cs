using GemManagment.BLL.Services.Implementaion;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.MemberShip;
using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GemManagment.PL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShip memberShip;
        private readonly ImemberService imemberService;
        private readonly IPlanService planService;

        public MemberShipController(IMemberShip memberShip , ImemberService imemberService , IPlanService planService )
        {
            this.memberShip = memberShip;
            this.imemberService = imemberService;
            this.planService = planService;
        }
        public IActionResult Index(string? search, int page = 1)
        {
            int pageSize = 5;

            var query = memberShip
                .GetAllMembers()
                .OrderBy(m => m.Member.Name)
                .AsQueryable();

            // 🔎 Server-side search (database level)
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m =>
                    m.Member.Name.Contains(search) ||
                    m.Member.Name.Contains(search));
            }

            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var memberships = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;
            return View(memberships);
        }
        public IActionResult Create()
        {
            var members = imemberService.GetAllMembrs();
            var memberList = new SelectList(members, "Id", "Name");
            var plans = planService.GetAllPlans();
            var planList = new SelectList(plans, "Id", "Name");
            ViewBag.members = memberList;
            ViewBag.plans = planList;



            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateMemberShipViewModel createMember)
        {

            if (!ModelState.IsValid)
            {
                return View(createMember);
            }
            
            var created = memberShip.Create(createMember);
            if (!created)
            {
                return View(createMember);
            }
            TempData["Success"] = "Created Successfuly";
            return RedirectToAction("Index");
        }

        public IActionResult Cancel(int memberid, int plansId)
        {
            var canceld = memberShip.Cancel(memberid, plansId);
            if (canceld)
            {
                TempData["Success"] = "Cancles";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Cancles not";

                return RedirectToAction("Index");

            }

                return View();
        }

    }
}

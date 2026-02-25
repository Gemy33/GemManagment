using GemManagment.BLL.Services.Interfaces;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.MemberSessionViewModels;

using Microsoft.AspNetCore.Mvc;

namespace GymManagment.PL.Controllers
{
    public class MemberSessionController(IUniteOfWork unitOfWork,ISessionService sessionService,IMemberSessionService memberSessionService ,ImemberService memberService):Controller
    {
        public IActionResult Index()
        {
            var sessions = sessionService.GetAllSessions();
            return View(sessions);
        }
        public IActionResult GetMembersForOngoingSessions(int sessionId)
        {
            if (sessionId <= 0) return RedirectToAction(nameof(Index));
           var members= memberSessionService.GetMembersForOngoingSessions(sessionId);
           TempData["sessionId"] = sessionId;
            return View(members);
        }

        public IActionResult GetMembersForUpComingSessions(int sessionId)
        {
            if(sessionId <= 0) return RedirectToAction(nameof(Index));
            var members = memberSessionService.GetMembersForUpComingSessions(sessionId);
            TempData["sessionId"] = sessionId;
            return View(members);
        }
        public IActionResult CreateBooking(int Id)
        {
            if (Id <= 0)
                return RedirectToAction(nameof(GetMembersForUpComingSessions));


            var session = sessionService.GetSessionById(Id);
            var membersNumber = memberSessionService.GetMembersForUpComingSessions(Id).Count();
            if (session == null || membersNumber == session.Capacity) {
                TempData["Error"] = "Session is Full Capacity Can not Add Members ";
                return RedirectToAction(nameof(GetMembersForUpComingSessions), new { sessionId = Id });
            }



            var members = memberService.GetAllMembrs().OrderBy(m=>m.Name);
          
             
            var model = new CreateBookingViewModel()
            {
                Members = members,
                sessionId = Id
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)

        {
            if (!ModelState.IsValid)
            {
                //VERY IMPORTANT
                TempData["Error"] = "Check Phone Must Be Valid Egyption Phone Number";
                model.Members = memberService.GetAllMembrs();
                return View("CreateBooking", model);
            }

            // Save booking here
            var membersession = new MemberSession()
            {
                MemberId = model.memberId,
                SessionId = model.sessionId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsAttended =false,
            };
            var membersInSession = memberSessionService.GetMembersForUpComingSessions(model.sessionId);
            if(membersInSession is not null && membersInSession.Any())
            {
                var res = membersInSession.FirstOrDefault(m => m.MemberId == membersession.MemberId);
                if(res is not null)
                {
                    TempData["Error"] = $"this member you need to Book in session is already Booked";
                    return RedirectToAction(nameof(GetMembersForUpComingSessions), new { sessionId = model.sessionId });
                }

            }
            unitOfWork.GetGenericRepo<MemberSession>().Add(membersession);
            var flag= unitOfWork.SaveChanges(); 
            if (flag > 0)
                TempData["Success"] = "Member Added To Session Successfuly";
            else
                TempData["Error"] = $"Member Feild To Booking Session Try Again";
            return RedirectToAction(nameof(GetMembersForUpComingSessions), new {sessionId=model.sessionId});
        }
        public IActionResult MarkAttended(int memberId,int sessionId)
        {
            if (memberId <= 0 || sessionId <= 0) return RedirectToAction("Index");
          var membersession=  memberSessionService.Get(memberId, sessionId);
            membersession.IsAttended = true;

            unitOfWork.GetGenericRepo<MemberSession>().Update(membersession);
            unitOfWork.SaveChanges();
            return RedirectToAction("GetMembersForOngoingSessions", new {sessionId= sessionId });
        }

        public IActionResult CancelModel(int sessionId,int memberId)
        {
            if(sessionId<=0|| memberId <= 0)
                return RedirectToAction(nameof(Index));

            ViewData["sessionId"] = sessionId;
            ViewData["memberId"] = memberId;
            return View(sessionId);
        }

        [HttpPost]
        public IActionResult Cancel(int sessionId,int memberId)
        {
            if(sessionId<=0 || memberId <= 0)
            {
                return RedirectToAction(nameof(Index));
            }
            memberSessionService.Delete(sessionId, memberId);
            TempData["Success"] = "member Deleted Successfuly";
            return RedirectToAction(nameof(GetMembersForUpComingSessions), new {sessionId=sessionId});
        }
    }
}

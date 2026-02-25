using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Analytic;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class AnlyticService : IAnlyticService
    {
        private readonly IUniteOfWork uniteOfWork;

        public AnlyticService(IUniteOfWork uniteOfWork)
        {
            this.uniteOfWork = uniteOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = uniteOfWork.SessionRepo.GetAll();
            return new AnalyticsViewModel
            {
                TotalMembers = uniteOfWork.GetGenericRepo<Member>().GetAll().Count(),
                TotalTrainers = uniteOfWork.GetGenericRepo<Trainer>().GetAll().Count(),
                ActiveMembers = uniteOfWork.GetGenericRepo<MemberPlan>().GetAll(c => c.Statuse == "Active").Count(),
                CompletedSessions = sessions.Count(s => s.EndDate < DateTime.Now),
                OngoingSessions = sessions.Count(s => s.StartDate > DateTime.Now),
                UpcomingSessions = sessions.Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now)
            };

        }
    }
}

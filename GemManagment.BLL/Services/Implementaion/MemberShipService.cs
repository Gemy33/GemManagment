using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.MemberShip;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class MemberShipService : IMemberShip
    {
        private readonly IUniteOfWork uniteOfWork;

        public MemberShipService(IUniteOfWork uniteOfWork)
        {
            this.uniteOfWork = uniteOfWork;
        }

        public bool Cancel(int memberid , int planid)
        {
            var membership = uniteOfWork.MemberShipRepo.GetMember(memberid , planid);
            if (membership is not null)
            {
                if (membership.Statuse == "Active")
                {
                    uniteOfWork.MemberShipRepo.Delete(membership);
                    return uniteOfWork.SaveChanges() > 0; 
                }

            }
            return false;
        }

        public bool Create(CreateMemberShipViewModel createmembership)
        {
            var plan = uniteOfWork.GetGenericRepo<Plans>().GetById(createmembership.PlanId);
            var mebership = new MemberPlan()
            {
                CreatedAt = DateTime.Now,
                PlansId = createmembership.PlanId,
                MemberId = createmembership.Memberid,
                EndDate = DateTime.Now.AddDays(plan.DurationInDays)


            };
            uniteOfWork.GetGenericRepo<MemberPlan>().Add(mebership);
            return uniteOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberPlan> GetAllMembers()
        {
            var members = uniteOfWork.MemberShipRepo.GetMembers();
            return members;
        }
    }
}

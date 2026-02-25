using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Plan;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class PlanService : IPlanService
    {
        private readonly IUniteOfWork uniteOfWork;

        public PlanService(IUniteOfWork uniteOfWork)
        {
            this.uniteOfWork = uniteOfWork;
        }
        public PlanToEditViewModel? DisplayToEdit(int planId)
        {
            var plan = uniteOfWork.GetGenericRepo<Plans>().GetById(planId);
            if (plan == null || plan.IsActive == false || HasActiveMemberPlan(planId))
                return null;

           

            return new PlanToEditViewModel
            {
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                DurationDays = plan.DurationInDays
            };
        }

        private bool HasActiveMemberPlan(int planId)
        {
            return uniteOfWork.GetGenericRepo<MemberPlan>()
                .GetAll(mp => mp.PlansId == planId && mp.Statuse == "Active")
                .Any();
        }

        public bool EditPlan(int planId, PlanToEditViewModel model)
        {
            var plan = uniteOfWork.GetGenericRepo<Plans>().GetById(planId);
            if (plan == null || HasActiveMemberPlan(planId))
                return false;
          
            try
            {
                plan.Name = model.Name;
                plan.Price = model.Price;
                plan.Description = model.Description;
                plan.DurationInDays = model.DurationDays;
                plan.UpdatedAt = DateTime.Now;
                uniteOfWork.GetGenericRepo<Plans>().Update(plan);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = uniteOfWork.GetGenericRepo<Plans>().GetAll().ToList();
            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                DurationDays = p.DurationInDays,
                IsActive = p.IsActive
            });
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var plan = uniteOfWork.GetGenericRepo<Plans>().GetById(id);
            if (plan == null)
                return null;
            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                DurationDays = plan.DurationInDays,
                IsActive = plan.IsActive

            };
        }

        public bool TogglePlanStatus(int planId)
        {
            var plan = uniteOfWork.GetGenericRepo<Plans>().GetById(planId);
            if (plan is null || HasActiveMemberPlan(planId))
                return false;
           
            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;
            try
            {
                uniteOfWork.GetGenericRepo<Plans>().Update(plan);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}

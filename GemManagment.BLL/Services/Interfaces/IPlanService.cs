using GemManagment.BLL.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanById(int id);

        PlanToEditViewModel? DisplayToEdit(int planId);

        bool EditPlan(int planId, PlanToEditViewModel model);

        bool TogglePlanStatus(int planId);
    }
}

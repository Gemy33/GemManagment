using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagment.BLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength (50,ErrorMessage ="Name must be less then 51 char")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Descriptoin is Required")]
        [StringLength(200, ErrorMessage = "Descriptoin must be less then 200 char")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Duration Days is Required")]
        [Range(1,365,ErrorMessage ="Duration Days must be between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage ="Price is required")]
        [Range(0.1,10000,ErrorMessage ="price must be between 1 and 10000")]
        public decimal Price { get; set; }

    }
}

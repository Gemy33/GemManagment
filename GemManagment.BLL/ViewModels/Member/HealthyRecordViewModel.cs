using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels.Member
{
    public class HealthyRecordViewModel
    {
        [Required(ErrorMessage ="The Height is Required")]
        [Range(1, 2, ErrorMessage = "Height must be between 1 and 2 meters")]
        public decimal Height { get; set; }
        
        [Required(ErrorMessage = "The Weight is Required")]
        [Range(10, 300, ErrorMessage = "Wight must be between 1 To 400 KG")]

        public decimal Weight { get; set; }
        [Required(ErrorMessage ="Blood Type must be Rquired")]
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }

    }
}

using GemManagment.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels.Trainer
{
    public class TrainerToCreateViewModel
    {
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;

        public GeneralFitness Specialty { get; set; }
    }
    
}

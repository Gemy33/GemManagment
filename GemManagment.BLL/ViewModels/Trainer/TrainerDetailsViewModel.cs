using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels.Trainer
{
    public class TrainerDetailsViewModel
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;

        public string Address { get; set; } = null!;

    }
}

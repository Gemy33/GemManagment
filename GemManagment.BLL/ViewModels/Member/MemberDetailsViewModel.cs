using GemManagment.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels.Member
{
    public class MemberDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Photo { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string PlaneName { get; set; } = null!;
        public string MembershipStartDate { get; set; } = null!;
        public string MembershipEndDate { get; set; } = null!;
        public string Address { get; set; } = null!;
        
    }
}

using GemManagment.BLL.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.ViewModels.MemberSessionViewModels
{
    public class CreateBookingViewModel
    {
        [Required(ErrorMessage ="Member Name is Required")]
        public int memberId { get; set; }
        public string? Name { get; set; }
        public int sessionId { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Formate")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be  Valid  Egyption PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }//for checking if member is found or not
        public IEnumerable<MemberViewModel>? Members { get; set; }
    }
}

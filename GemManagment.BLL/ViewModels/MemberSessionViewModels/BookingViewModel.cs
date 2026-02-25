using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.ViewModels.MemberSessionViewModels
{
    public class BookingViewModel
    {
        public int memberId { get; set; }
        public int sessionId { get; set; }

        public string Phone { get; set; }
    }
}

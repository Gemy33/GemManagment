using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.ViewModels.MemberSessionViewModels
{
    public class UpComingSessionViewModel
    {

        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BookingDate { get; set; }

        public bool IsAttended { get; set; }

    }
}


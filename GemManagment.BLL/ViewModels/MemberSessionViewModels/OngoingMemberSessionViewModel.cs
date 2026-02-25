using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.ViewModels.MemberSessionViewModels
{
    public class OngoingMemberSessionViewModel
    {
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public string Name { get; set; } = null!;

        public bool IsAttended { get; set; }
    }
}

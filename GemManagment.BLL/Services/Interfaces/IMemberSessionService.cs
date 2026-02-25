using GemManagment.DAL.Models;
using GymManagment.BLL.ViewModels.MemberSessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface IMemberSessionService
    {
        public IEnumerable<OngoingMemberSessionViewModel> GetMembersForOngoingSessions(int sessionId);
        public MemberSession Get(int memberId, int sessionId);
        public IEnumerable<UpComingSessionViewModel> GetMembersForUpComingSessions(int sessionId);

        bool Delete(int sessionId, int memberId);
    }
}

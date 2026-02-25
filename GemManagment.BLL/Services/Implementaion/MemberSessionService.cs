using GemManagment.DAL.Models;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.MemberSessionViewModels;
using GymManagment.DAL.Repositotys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
    public class MemberSessionService(ImemberSessionRepository memberSessionRepository) : IMemberSessionService
    {
        public bool Delete(int sessionId, int memberId)
        {
          return memberSessionRepository.Delete(sessionId, memberId);
        }

        public MemberSession Get(int memberId, int sessionId)
        {
            return memberSessionRepository.Get(memberId, sessionId);
        }

        public IEnumerable<OngoingMemberSessionViewModel> GetMembersForOngoingSessions(int sessionId)
        {
            var members = memberSessionRepository.GetMemberSessionsIncludeMembers(sessionId);

            return members.Select(m => new OngoingMemberSessionViewModel()
            {
                MemberId = m.MemberId,
                SessionId = m.SessionId,
                Name = m.Member.Name,
                IsAttended = m.IsAttended,
            });
        }
        public IEnumerable<UpComingSessionViewModel> GetMembersForUpComingSessions(int sessionId)
        {
            var members = memberSessionRepository.GetMemberSessionsIncludeMembers(sessionId);
            return members.Select(m => new UpComingSessionViewModel()
            {
                BookingDate=m.CreatedAt,
                MemberId = m.MemberId,
                SessionId = m.SessionId,
                Name = m.Member.Name,
                IsAttended = m.IsAttended,
            });
        }
    }
}


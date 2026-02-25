using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositotys.Interfaces
{
    public interface ImemberSessionRepository:IGenericRepo<MemberSession>
    {
        public IEnumerable<MemberSession> GetMemberSessionsIncludeMembers(int sessionId);
        public MemberSession Get(int memberId, int sessionId);
        public bool Delete(int sessionId, int memberId);
    }
}

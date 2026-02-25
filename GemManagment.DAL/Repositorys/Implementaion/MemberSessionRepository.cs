
using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Implementaion;
using GymManagment.DAL.Repositotys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositotys.Classes
{
    public class MemberSessionRepository(GemDbcontext context) : GenericRepo<MemberSession>(context), ImemberSessionRepository
    {
        private readonly GemDbcontext context = context;

        public bool Delete(int sessionId, int memberId)
        {
            var member=Get(memberId, sessionId);
            context.Remove(member);
            return context.SaveChanges() > 0;
        }

        public MemberSession Get(int memberId, int sessionId)
        {
            var membersession = context.MemberSession.Where(ms => ms.MemberId == memberId && ms.SessionId == sessionId).FirstOrDefault();
            return membersession;
        }

        public IEnumerable<MemberSession> GetMemberSessionsIncludeMembers(int sessionId)
        {
            return context.MemberSession.Where(MS => MS.SessionId == sessionId).Include(ms => ms.Member).ToList();
        }
    }
}

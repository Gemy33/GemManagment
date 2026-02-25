using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Implementaion
{
    public class SessionRepo : GenericRepo<Session>, ISessionRepo
    {
        private readonly GemDbcontext dbcontext;

        public SessionRepo(GemDbcontext dbcontext):base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public IEnumerable<Session> GetAllSessionWithCategoryAndTrainer(Func<Session,bool>?condition)
        {
            if (condition != null)
            {
                return dbcontext.Session.Include(s => s.Category).Include(s => s.Trainer).Where(condition).ToList();
            }
            return dbcontext.Session.Include(s => s.Category).Include(s => s.Trainer).ToList();
        }

        public int GetAvailableSlots(int sessionId)
        {
           var session = dbcontext.Session.Find(sessionId);
            if (session == null)
            {
                throw new ArgumentException("Invalid session ID");
            }
            var bookedSlots = dbcontext.MemberSession.Count(ms => ms.SessionId == sessionId);
            return session.Capacity - bookedSlots;
        }
    }
}

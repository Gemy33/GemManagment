using GemManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Interfaces
{
    public interface ISessionRepo : IGenericRepo<Session>
    {
        IEnumerable<Session> GetAllSessionWithCategoryAndTrainer(Func<Session,bool>?cirti = null);

        int GetAvailableSlots(int sessionId);
    }
}

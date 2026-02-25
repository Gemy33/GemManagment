using GemManagment.BLL.ViewModels.Session;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);

        bool CreateSession(CreateSessionViewModel model);

        UpdateSessionViewModel? GetSessionForUpdate(int id);

        bool UpdateSession(int id, UpdateSessionViewModel model);

        bool DeleteSession(int id);
        IEnumerable<CategoryDropDownViewModel> GetCategoryDropDownViewModels();
        IEnumerable<TrainerDropDownViewModel> GetTrainerDropDownViewModels();
    }
}

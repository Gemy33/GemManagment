using GemManagment.BLL.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(TrainerToCreateViewModel trainerToCreateView);

        TrainerDetailsViewModel? GetTrainerById(int trainerId);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdateView);

        bool DeleteTrainer(int trainerId);
        bool IsPhoneExist(string phone);
        bool IsEmailExist(string email);


    }
}

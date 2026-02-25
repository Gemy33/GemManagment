using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Trainer;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class TrainerService : ITrainerService
    {
        private readonly IUniteOfWork uniteOfWork;

        public TrainerService(IUniteOfWork uniteOfWork)
        {
            this.uniteOfWork = uniteOfWork;
        }
        public bool CreateTrainer(TrainerToCreateViewModel trainerToCreateView)
        {
            if (IsEmailExist(trainerToCreateView.Email) || IsPhoneExist(trainerToCreateView.PhoneNumber))
                return false;
            var trainer = new Trainer
            {
                Name = trainerToCreateView.Name,
                Phone = trainerToCreateView.PhoneNumber,
                Email = trainerToCreateView.Email,
                DataOfBarth = trainerToCreateView.DateOfBirth,
                Address = new Addrees
                {
                    BuildingNumber = trainerToCreateView.BuildingNumber.ToString(),
                    Street = trainerToCreateView.Street,
                    City = trainerToCreateView.City
                },
                Specialty = trainerToCreateView.Specialty,
                Gender = trainerToCreateView.Gender,


            };
            try
            {
                uniteOfWork.GetGenericRepo<Trainer>().Add(trainer);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteTrainer(int trainerId)
        {
            var trainer = uniteOfWork.GetGenericRepo<Trainer>().GetById(trainerId);
            var sessions = uniteOfWork.GetGenericRepo<Session>().GetAll(s => s.TrainerId == trainerId && s.StartDate > DateTime.Now).Any();
            if (trainer is null || sessions)
                return false;
            try
            {
                uniteOfWork.GetGenericRepo<Trainer>().Delete(trainer);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = uniteOfWork.GetGenericRepo<Trainer>().GetAll();
            if (trainers is null || !trainers.Any())
                return [];
            var trainerViews = trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                PhoneNumber = t.Phone,
                Email = t.Email,
                Specialty = t.Specialty.ToString()


            });
            return trainerViews;
        }

        public TrainerDetailsViewModel? GetTrainerById(int trainerId)
        {
            var trainer = uniteOfWork.GetGenericRepo<Trainer>().GetById(trainerId);
            if (trainer is null)
                return null;
            return new TrainerDetailsViewModel
            {
                Email = trainer.Email,
                Name = trainer.Name,
                PhoneNumber = trainer.Phone,
                Specialty = trainer.Specialty.ToString(),
                DateOfBirth = trainer.DataOfBarth.ToString("yyyy-MM-dd"),
                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}"
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = uniteOfWork.GetGenericRepo<Trainer>().GetById(trainerId);
            if (trainer is null)
                return null;
            return new TrainerToUpdateViewModel
            {
                BuildingNumber = int.Parse(trainer.Address.BuildingNumber),
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                Email = trainer.Email,
                Name = trainer.Name,
                Phone = trainer.Phone,
                Spicaialty = trainer.Specialty
            };
        }

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdateView)
        {
            var trainer = uniteOfWork.GetGenericRepo<Trainer>().GetById(trainerId);
            if (trainer is null)
                return false;
            trainer.Name = trainerToUpdateView.Name;
            trainer.Phone = trainerToUpdateView.Phone;
            trainer.Email = trainerToUpdateView.Email;
            trainer.Specialty = trainerToUpdateView.Spicaialty;
            trainer.Address.City = trainerToUpdateView.City;
            trainer.Address.Street = trainerToUpdateView.Street;
            trainer.Address.BuildingNumber = trainerToUpdateView.BuildingNumber.ToString();
            trainer.UpdatedAt = DateTime.Now;
            try
            {
                uniteOfWork.GetGenericRepo<Trainer>().Update(trainer);
                return uniteOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

       public bool IsEmailExist(string email)
        {
            return uniteOfWork.GetGenericRepo<Trainer>().GetAll(t => t.Email == email).Any();

        }
       public bool IsPhoneExist(string phone)
        {
            return uniteOfWork.GetGenericRepo<Trainer>().GetAll(t => t.Phone == phone).Any();
        }
    }

}

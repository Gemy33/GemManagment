using AutoMapper;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Session;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class SessionService : ISessionService
    {
        private readonly IUniteOfWork uniteOfWork;

        public IMapper Mapper { get; }

        public SessionService(IUniteOfWork uniteOfWork, IMapper mapper)
        {
            this.uniteOfWork = uniteOfWork;
            Mapper = mapper;
        }
        public bool CreateSession(CreateSessionViewModel model)
        {
            try
            {
                if (ValidateSeesionForCreate(model))
                {

                    var session = Mapper.Map<Session>(model);
                    uniteOfWork.GetGenericRepo<Session>().Add(session);
                    return uniteOfWork.SaveChanges() > 0;

                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }




        public bool DeleteSession(int id)
        {
            try
            {
                var session = uniteOfWork.GetGenericRepo<Session>().GetById(id);
                if (!ValidateSessionForRemove(session!))
                {
                    return false;
                }
                uniteOfWork.GetGenericRepo<Session>().Delete(session);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = uniteOfWork.SessionRepo.GetAllSessionWithCategoryAndTrainer();
            if (sessions == null || !sessions.Any())
            {
                return Enumerable.Empty<SessionViewModel>();
            }

            var viewModels = Mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach (var se in viewModels)
            {
                se.AvailableSlots = uniteOfWork.SessionRepo.GetAvailableSlots(se.Id);
            }
            return viewModels;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var session = uniteOfWork.SessionRepo.GetAllSessionWithCategoryAndTrainer(x => x.Id == id).FirstOrDefault();
            if (session == null)
            {
                return null;
            }

            var viewModel = Mapper.Map<SessionViewModel>(session);

            return viewModel;

        }

        public UpdateSessionViewModel? GetSessionForUpdate(int id)
        {
            var session = uniteOfWork.SessionRepo.GetById(id);

            if (!ValidateSessionForUpdate(session!))
            {
                return null;
            }
            var viewModel = Mapper.Map<UpdateSessionViewModel>(session);

            return viewModel;
        }

        public bool UpdateSession(int id, UpdateSessionViewModel model)
        {
            var session = uniteOfWork.GetGenericRepo<Session>().GetById(id);
            if (!ValidateSessionForUpdate(session!))
            {
                return false;
            }
            session.Description = model.Description;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.TrainerId = model.TrainerId;
            session.UpdatedAt = DateTime.Now;
            uniteOfWork.GetGenericRepo<Session>().Update(session);
            return uniteOfWork.SaveChanges() > 0;

        }


        public IEnumerable<CategoryDropDownViewModel> GetCategoryDropDownViewModels()
        {
            return uniteOfWork.GetGenericRepo<Category>().GetAll().Select(c => new CategoryDropDownViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public IEnumerable<TrainerDropDownViewModel> GetTrainerDropDownViewModels()
        {
            return uniteOfWork.GetGenericRepo<Trainer>().GetAll().Select(t => new TrainerDropDownViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
        }

        #region Validate Session 
        private bool ValidateSeesionForCreate(CreateSessionViewModel model)
        {
            if (model.Capacity < 0 || model.Capacity > 25)
                return false;
            if (model.StartDate >= model.EndDate)
                return false;
            var trainer = uniteOfWork.GetGenericRepo<Trainer>().GetById(model.TrainerId);
            if (trainer == null)
                return false;
            
            var category = uniteOfWork.GetGenericRepo<Category>().GetById(model.CategoryId);
            if (category == null) return false;
            return true;
        }

        private bool ValidateSessionForUpdate(Session model)
        {
            if (model is null) return false;

            // completed
            if (model.EndDate < DateTime.Now)
                return false;

            // started
            if (model.StartDate <= DateTime.Now & model.EndDate > DateTime.Now)
                return false;

            var HasActiveBooking = uniteOfWork.SessionRepo.GetAvailableSlots(model.Id) > 0;

            return true;
        }

        private bool ValidateSessionForRemove(Session model)
        {
            if (model is null) return false;

            // Upcoming
            if (model.StartDate > DateTime.Now)
                return false;

            // started
            if (model.StartDate <= DateTime.Now & model.EndDate > DateTime.Now)
                return false;

            var HasActiveBooking = uniteOfWork.SessionRepo.GetAvailableSlots(model.Id) > 0;

            return true;
        }
        private bool IsEmailUnique(string email)
        {
            var existingTrainer = uniteOfWork.GetGenericRepo<Trainer>().GetAll(t => t.Email == email).FirstOrDefault();
            return existingTrainer == null;
        }
        private bool IsPhoneUnique(string phone)
        {
            var existingTrainer = uniteOfWork.GetGenericRepo<Trainer>().GetAll(t => t.Phone == phone).FirstOrDefault();
            return existingTrainer == null;
        }


        #endregion
    }
}

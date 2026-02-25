using AutoMapper;
using GemManagment.BLL.ViewModels;
using GemManagment.DAL.Models;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateSessionViewModel, Session>().ReverseMap();
            CreateMap<Session, UpdateSessionViewModel>();

            CreateMap<Category, CategoryViewModel>()
                .ForMember(des => des.SessionCount, opt => opt.MapFrom(src => src.sessions.Count())).ReverseMap();
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());
        }
    }
}

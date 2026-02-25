using AutoMapper;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUniteOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<CategoryViewModel> GetAllCategories()
        {
            var categories = _unitOfWork.GetGenericRepo<Category>().GetAll();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        public CategoryViewModel GetCategoryById(int id)
        {
            var category = _unitOfWork.GetGenericRepo<Category>().GetById(id);

            if (category == null)
            {
                throw new InvalidOperationException("Category not found");
            }

            return _mapper.Map<CategoryViewModel>(category);
        }

        public List<SessionViewModel> GetSessionsByCategory(int categoryId)
        {
            var sessions = _unitOfWork.GetGenericRepo<Session>().GetAll()
                .Where(s => s.CategoryId == categoryId)
                .OrderBy(s => s.StartDate)
                .ToList();

            return _mapper.Map<List<SessionViewModel>>(sessions);
        }

        public int GetSessionCountByCategory(int categoryId)
        {
            return _unitOfWork.GetGenericRepo<Session>().GetAll()
                .Where(s => s.CategoryId == categoryId)
                .Count();
        }

        List<CategoryViewModel> ICategoryService.GetAllCategories()
        {
            throw new NotImplementedException();
        }

    }
}

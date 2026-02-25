using GemManagment.BLL.ViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetAllCategories();
        CategoryViewModel GetCategoryById(int id);
        List<SessionViewModel> GetSessionsByCategory(int categoryId);
        int GetSessionCountByCategory(int categoryId);
    }
}

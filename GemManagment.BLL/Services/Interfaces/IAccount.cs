using GemManagment.BLL.ViewModels.Account;
using GemManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface IAccount
    {
        Task<ApplicationUser?> ValidatedUser(LoginViewModel model);
    }
}

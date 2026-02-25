using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Account;
using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    public class AccountService : IAccount
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ApplicationUser?> ValidatedUser(LoginViewModel model)
        {
            var ExistUser = await userManager.FindByEmailAsync(model.Email);
            if (ExistUser is not null)
            {
                var validpasword = await userManager.CheckPasswordAsync(ExistUser, model.Password);
                if (validpasword)
                {
                    return ExistUser;
                }
            }
            return null;
        }
    }
}

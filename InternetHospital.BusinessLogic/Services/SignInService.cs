using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Services
{
    public class SignInService : ISignInService
    {
        public async Task<(User user, bool state)> CheckIfExist(UserLoginModel model, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameOrEmailAsync(model.UserName);
            if (await userManager.IsEmailConfirmedAsync(user) && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return (user, true);
            }
            else
            {
                return (user, false);
            }
        }
    }
}

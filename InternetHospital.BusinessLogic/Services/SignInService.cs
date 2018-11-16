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
        public async Task<bool> CheckIfExist(User user, UserLoginModel model, UserManager<User> userManager)
        {
            bool result;
            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                result = false;
            }
            else if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}

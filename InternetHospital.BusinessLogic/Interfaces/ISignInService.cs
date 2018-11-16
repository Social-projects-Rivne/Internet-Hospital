using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface ISignInService
    {
        Task<bool> CheckIfExist(User user, UserLoginModel model, UserManager<User> userManager);
    }
}
